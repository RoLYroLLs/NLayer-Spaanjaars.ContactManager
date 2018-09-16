using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spaanjaars.Infrastructure;

namespace Spaanjaars.ContactManager45.Tests.Unit
{
  [TestClass]
  [ExcludeFromCodeCoverage]
  public class DomainEntityTests : UnitTestBase
  {
    #region Nested helpers

    internal class PersonWithIntAsId : DomainEntity<int>
    {
      public override System.Collections.Generic.IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
      {
        throw new NotImplementedException();
      }
    }

    internal class PersonWithGuidAsId : DomainEntity<Guid>
    {
      public override System.Collections.Generic.IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
      {
        throw new NotImplementedException();
      }
    }
    #endregion

    [TestMethod]
    public void NewPersonWithIntAsIdIsTransient()
    {
      var person = new PersonWithIntAsId();
      person.IsTransient().Should().BeTrue();
    }

    [TestMethod]
    public void PersonWithIntAsIdWithValueIsNotTransient()
    {
      var person = new PersonWithIntAsId { Id = 4 };
      person.IsTransient().Should().BeFalse();
    }

    [TestMethod]
    public void NewPersonWithGuidAsIdIsTransient()
    {
      var person = new PersonWithGuidAsId();
      person.Id.Should().Be(Guid.Empty);
      person.IsTransient().Should().BeTrue();
    }

    [TestMethod]
    public void PersonWithGuidAsIdWithValueIsNotTransient()
    {
      var person = new PersonWithGuidAsId { Id = Guid.NewGuid() };
      person.IsTransient().Should().BeFalse();
    }

    [TestMethod]
    public void SameIdentityProduceEqualsTrueTest()
    {
      var entityLeft = new PersonWithIntAsId { Id = 1 };
      var entityRight = new PersonWithIntAsId { Id = 1 };

      //Act
      bool resultOnEquals = entityLeft.Equals(entityRight);
      bool resultOnOperator = entityLeft == entityRight;

      //Assert
      resultOnEquals.Should().BeTrue();
      resultOnOperator.Should().BeTrue();
    }

    [TestMethod]
    public void DifferentIdProduceEqualsFalseTest()
    {
      //Arrange
      var entityLeft = new PersonWithIntAsId { Id = 1 };
      var entityRight = new PersonWithIntAsId { Id = 2 };

      //Act
      bool resultOnEquals = entityLeft.Equals(entityRight);
      bool resultOnOperator = entityLeft == entityRight;

      //Assert
      resultOnEquals.Should().BeFalse();
      resultOnOperator.Should().BeFalse();
    }

    [TestMethod]
    public void CompareUsingEqualsOperatorsAndNullOperandsTest()
    {
      //Arrange

      PersonWithIntAsId entityLeft = null;
      PersonWithIntAsId entityRight = new PersonWithIntAsId { Id = 2 };

      //Act
      if (!(entityLeft == (PersonWithIntAsId)null))
        Assert.Fail();

      if (!(entityRight != (PersonWithIntAsId)null))
        Assert.Fail();

      entityRight = null;

      //Act
      if (!(entityLeft == entityRight))
        Assert.Fail();

      if (entityLeft != entityRight)
        Assert.Fail();


    }
    [TestMethod]
    public void CompareTheSameReferenceReturnTrueTest()
    {
      //Arrange
      var entityLeft = new PersonWithIntAsId();
      PersonWithIntAsId entityRight = entityLeft;

      //Act
      if (!entityLeft.Equals(entityRight))
      {
        Assert.Fail();
      }

      if (!(entityLeft == entityRight))
      {
        Assert.Fail();
      }
    }
    [TestMethod]
    public void CompareWhenLeftIsNullAndRightIsNullReturnFalseTest()
    {
      //Arrange
      PersonWithIntAsId entityLeft = null;
      var entityRight = new PersonWithIntAsId { Id = 1 };

      //Act
      if (!(entityLeft == (PersonWithIntAsId)null))//this perform ==(left,right)
      {
        Assert.Fail();
      }

      if (!(entityRight != (PersonWithIntAsId)null))//this perform !=(left,right)
      {
        Assert.Fail();
      }
    }

    [TestMethod]
    public void SetIdentitySetANonTransientEntity()
    {
      //Arrange
      var entity = new PersonWithIntAsId { Id = 1 };

      //Assert
      Assert.IsFalse(entity.IsTransient());
    }
  }
}
