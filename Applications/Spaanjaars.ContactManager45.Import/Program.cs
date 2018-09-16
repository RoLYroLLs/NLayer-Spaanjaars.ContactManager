using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AutoMapper;
using FileHelpers;
using Spaanjaars.ContactManager45.Model;
using Spaanjaars.ContactManager45.Repositories.EF;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Spaanjaars.ContactManager.Import
{
  class Program
  {
    private static readonly PeopleRepository PeopleRepository = new PeopleRepository();
    private static string _fileName = Path.Combine(Environment.CurrentDirectory, "People.csv");

    /// <summary>
    /// Initializes a new instance of the Program class.
    /// </summary>
    static Program()
    {
      AutoMapperConfig.Start();
      Console.BackgroundColor = ConsoleColor.White;
      Console.ForegroundColor = ConsoleColor.Black;
      Console.Clear();
      // Use LocalDB for Entity Framework by default
      Database.DefaultConnectionFactory = new SqlConnectionFactory("Data Source=(localdb)\\v11.0; Integrated Security=True; MultipleActiveResultSets=True");
    }

    static void Main(string[] args)
    {
      if (args.Length == 1)
      {
        _fileName = args[0];
      }
      using (var engine = new FileHelperAsyncEngine<ImportPerson>())
      {
        engine.BeginReadFile(_fileName);
        int success = 0;
        int failed = 0;
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        using (var uow = new EFUnitOfWorkFactory().Create())
        {
          foreach (ImportPerson importPerson in engine)
          {
            try
            {
              var person = new Person();
              Mapper.Map(importPerson, person);
              TryAddEmailAddress(importPerson, person);
              TryAddPhoneNumber(importPerson, person);
              person.HomeAddress = FixAddress(person.HomeAddress, changeMissingDataToNull: false); // False sets missing data to <unknown>.
              person.WorkAddress = FixAddress(person.WorkAddress, changeMissingDataToNull: false); // False sets missing data to <unknown>.
              if (!person.Validate().Any())
              {
                success++;
                PeopleRepository.Add(person);
                if (success % 30 == 0)
                {
                  uow.Commit(true);
                }
              }
              else
              {
                //TODO Handle invalid data. Log it, or deal with it in some way.
                failed++;
              }
            }
            catch (Exception ex)
            {
              //TODO Handle error. Log it, or deal with it in some way.
              Console.WriteLine("Failed to import row " + engine.LineNumber);
              Console.WriteLine("Error " + ex.Message);
              failed++;
            }
            long elapsed = stopwatch.ElapsedMilliseconds;
            double timeForOne = (elapsed / (double)success) / 1000;
            double perSecond = 1 / timeForOne;
            UpdateUI(success, failed, elapsed, perSecond, importPerson);
          }
        }
        stopwatch.Stop();
        engine.Close();
      }
    }

    /// <summary>
    /// Adds an e-mail address when the source text is not null and not the text NULL.
    /// </summary>
    private static void TryAddEmailAddress(ImportPerson importPerson, Person person)
    {
      if (!string.IsNullOrEmpty(importPerson.Email) && importPerson.Email != "NULL")
      {
        person.EmailAddresses.Add(importPerson.Email, ConvertType(importPerson.EmailType));
      }
    }

    /// <summary>
    /// Adds a phone number when the source text is not null and not the text NULL.
    /// </summary>
    private static void TryAddPhoneNumber(ImportPerson importPerson, Person person)
    {
      if (!string.IsNullOrEmpty(importPerson.PhoneNumber) && importPerson.PhoneNumber != "NULL")
      {
        person.PhoneNumbers.Add(importPerson.PhoneNumber, ContactType.Business);
      }
    }

    /// <summary>
    /// Handles conversion to a ContactType. Returns None when source is empty or null, Business when source
    /// is Company and Personal when it's Private. Throws an exception in all other cases.
    /// </summary>
    private static ContactType ConvertType(string emailType)
    {
      if (string.IsNullOrEmpty(emailType) || emailType == "NULL")
      {
        return ContactType.None;
      }
      switch (emailType.ToLower())
      {
        case "company":
          return ContactType.Business;
        case "private":
          return ContactType.Personal;
        default:
          throw new Exception(String.Format("Unknown emailType {0}.", emailType));
      }
    }

    /// <summary>
    /// Fixes an address when it has missing or invalid data.
    /// </summary>
    /// <param name="address">The address to fix.</param>
    /// <param name="changeMissingDataToNull">When true, an empty string or the text NULL is converted to null; otherwise it's converted to &lt;Unknown&gt;</param>
    private static Address FixAddress(Address address, bool changeMissingDataToNull)
    {
      string street = SetValueToNullOrUnknown(address.Street, changeMissingDataToNull);
      string zipCode = SetValueToNullOrUnknown(address.ZipCode, changeMissingDataToNull);
      string city = SetValueToNullOrUnknown(address.City, changeMissingDataToNull);
      string country = SetValueToNullOrUnknown(address.Country, changeMissingDataToNull);
      return new Address(street, city, zipCode, country, address.ContactType);
    }

    private static string SetValueToNullOrUnknown(string value, bool changeMissingDataToNull)
    {
      string temp = changeMissingDataToNull ? null : "<Unknown>";
      return string.IsNullOrEmpty(value) || value == "NULL" ? temp : value;
    }

    /// <summary>
    /// Draws statistics on the screen.
    /// </summary>
    private static void UpdateUI(int success, int failed, long elapsed, double perSecond, ImportPerson importPerson)
    {
      Console.SetCursorPosition(0, 0);
      Console.WriteLine(string.Format("Currently importing:     {0} {1}", importPerson.FirstName, importPerson.LastName).PadRight(79));
      Console.WriteLine(string.Format("Records per second:      {0}", Convert.ToInt32(perSecond)).PadRight(79));
      TimeSpan timespan = TimeSpan.FromSeconds(Convert.ToInt32(elapsed / 1000));
      Console.WriteLine(string.Format("Running for:             {0}", timespan.ToString("c")).PadRight(79));
      Console.WriteLine("Successfully imported:   {0}", success);
      Console.WriteLine("Failed to import:        {0}", failed);
    }
  }
}