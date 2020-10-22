﻿using System;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;
using Xunit;

namespace Xamarin.CommunityToolkit.UnitTests.Helpers
{
	public class BindablePropertyBuilder_Tests
	{
		// property name
		[Fact]
		public void InitializeWithPropertyNameShouldSetBindablePropertyPropertyName()
		{
			var propertyName = "asdf";

			var bindableProperty = new BindablePropertyBuilder()
				.SetPropertyName(propertyName)
				.SetReturnType<string>()
				.SetDeclaringType<object>()
				.Build();

			Assert.Equal(propertyName, bindableProperty.PropertyName);
		}

		// return type
		[Fact]
		public void SetReturnTypeShouldSetBindablePropertyReturnType()
		{
			var returnType = typeof(string);

			var bindableProperty = new BindablePropertyBuilder()
				.SetPropertyName("asdf")
				.SetReturnType(returnType)
				.SetDeclaringType<object>()
				.Build();

			Assert.Equal(returnType, bindableProperty.ReturnType);
		}

		[Fact]
		public void SetReturnTypeGenericShouldSetBindablePropertyReturnType()
		{
			var bindableProperty = new BindablePropertyBuilder()
				.SetPropertyName("asdf")
				.SetReturnType<string>()
				.SetDeclaringType<object>()
				.Build();

			Assert.Equal(typeof(string), bindableProperty.ReturnType);
		}

		// declaring type
		[Fact]
		public void SetDeclaringTypeShouldSetBindablePropertyDeclaringType()
		{
			var declaringType = typeof(int);

			var bindableProperty = new BindablePropertyBuilder()
				.SetPropertyName("asdf")
				.SetReturnType<string>()
				.SetDeclaringType(declaringType)
				.Build();

			Assert.Equal(declaringType, bindableProperty.DeclaringType);
		}

		[Fact]
		public void SetDeclaringTypeGenericShouldSetBindablePropertyDeclaringType()
		{
			var bindableProperty = new BindablePropertyBuilder()
				.SetPropertyName("asdf")
				.SetReturnType<string>()
				.SetDeclaringType<object>()
				.Build();

			Assert.Equal(typeof(object), bindableProperty.DeclaringType);
		}

		// default value
		[Fact]
		public void SetDefaultValueShouldSetBindablePropertyDefaultValue()
		{
			var defaultValue = Guid.NewGuid();

			var bindableProperty = new BindablePropertyBuilder()
				.SetPropertyName("guid")
				.SetReturnType<Guid>()
				.SetDeclaringType<object>()
				.SetDefaultValue(defaultValue)
				.Build();

			Assert.Equal(defaultValue, bindableProperty.DefaultValue);
		}

		// default binding mode
		[Fact]
		public void SetDefaultBindingModeShouldSetBindablePropertyDefaultBindingMode()
		{
			var defaultBindingMode = BindingMode.TwoWay;

			var bindableProperty = new BindablePropertyBuilder()
				.SetPropertyName("asdf")
				.SetReturnType<object>()
				.SetDeclaringType<object>()
				.SetDefaultBindingMode(defaultBindingMode)
				.Build();

			Assert.Equal(defaultBindingMode, bindableProperty.DefaultBindingMode);
		}

		// validate value delegate
		[Fact]
		public void SetValidateValueDelegateShouldSetBindablePropertyValidateValueDelegate()
		{
			var hasRaisedDelegate = false;
			var validateValueDelegate = new BindableProperty.ValidateValueDelegate((bo, obj) =>
			{
				hasRaisedDelegate = true;
				return true;
			});

			_ = new TestControl
			{
				TestValueProperty = new BindablePropertyBuilder()
					.SetPropertyName(nameof(TestControl.TestValue))
					.SetReturnType<int>()
					.SetDeclaringType<TestControl>()
					.SetValidateValueDelegate(validateValueDelegate)
					.Build(),
				TestValue = 123
			};

			Assert.True(hasRaisedDelegate);
		}

		// property changed delegate
		[Fact]
		public void SetPropertyChangedDelegateShouldSetBindablePropertyPropertyChangedDelegate()
		{
			var hasRaisedDelegate = false;
			var propertyChangedDelegate = new BindableProperty.BindingPropertyChangedDelegate((bo, old, @new) =>
			{
				hasRaisedDelegate = true;
			});

			_ = new TestControl
			{
				TestValueProperty = new BindablePropertyBuilder()
					.SetPropertyName(nameof(TestControl.TestValue))
					.SetReturnType<int>()
					.SetDeclaringType<TestControl>()
					.SetPropertyChangedDelegate(propertyChangedDelegate)
					.Build(),
				TestValue = 123
			};

			Assert.True(hasRaisedDelegate);
		}

		// property changing delegate
		[Fact]
		public void SetPropertyChangingDelegateShouldSetBindablePropertyChangingDelegate()
		{
			var hasRaisedDelegate = false;
			var propertyChangingDelegate = new BindableProperty.BindingPropertyChangingDelegate((bo, old, @new) =>
			{
				hasRaisedDelegate = true;
			});

			_ = new TestControl
			{
				TestValueProperty = new BindablePropertyBuilder()
					.SetPropertyName(nameof(TestControl.TestValue))
					.SetReturnType<int>()
					.SetDeclaringType<TestControl>()
					.SetPropertyChangingDelegate(propertyChangingDelegate)
					.Build(),
				TestValue = 123
			};

			Assert.True(hasRaisedDelegate);
		}

		// coerce value delegate
		[Fact]
		public void SetCoerceValueDelegateShouoldSetBindablePropertyCoerceValueDelegate()
		{
			var hasRaisedDelegate = false;
			var coerceValueDelegate = new BindableProperty.CoerceValueDelegate((bo, @value) =>
			{
				hasRaisedDelegate = true;
				return null;
			});

			_ = new TestControl
			{
				TestValueProperty = new BindablePropertyBuilder()
					.SetPropertyName(nameof(TestControl.TestValue))
					.SetReturnType<int>()
					.SetDeclaringType<TestControl>()
					.SetCoerceValueDelegate(coerceValueDelegate)
					.Build(),
				TestValue = 123
			};

			Assert.True(hasRaisedDelegate);
		}

		// create default value delegate
		[Fact]
		public void SetCreateDefaultValueDelegateShouldSetBindablePropertyCreateDefaultValueDelegate()
		{
			var hasRaisedDelegate = false;
			var createDefaultValueDelegate = new BindableProperty.CreateDefaultValueDelegate((bo) =>
			{
				hasRaisedDelegate = true;
				return null;
			});

			_ = new TestControl
			{
				TestValueProperty = new BindablePropertyBuilder()
					.SetPropertyName(nameof(TestControl.TestValue))
					.SetReturnType<int>()
					.SetDeclaringType<TestControl>()
					.SetCreateDefaultValueDelegate(createDefaultValueDelegate)
					.Build(),
				TestValue = 123
			};

			Assert.True(hasRaisedDelegate);
		}

		// build
		[Theory]
		[InlineData(true)]
		[InlineData(false)]
		public void BuildShouldThrowAnExceptionIfBindablePropertyPropertyNameIsNullOrEmpty(bool isNull)
		{
			var propertyName = isNull ? null : string.Empty;

			var buildAction = new Action(() =>
			{
				_ = new BindablePropertyBuilder()
					.SetPropertyName(propertyName)
					.SetReturnType<string>()
					.SetDeclaringType<object>()
					.Build();
			});

			Assert.Throws<ArgumentNullException>(buildAction);
		}

		[Fact]
		public void BuildShouldThrowAnExceptionIfBindablePropertyReturnTypeIsNull()
		{
			var buildAction = new Action(() =>
			{
				_ = new BindablePropertyBuilder()
					.SetPropertyName("asdf")
					.SetReturnType(null)
					.SetDeclaringType<object>()
					.Build();
			});

			Assert.Throws<ArgumentNullException>(buildAction);
		}

		[Fact]
		public void BuildShouldThrowAnExceptionIfSetReturnTypeGenericIsNotCalled()
		{
			var buildAction = new Action(() =>
			{
				_ = new BindablePropertyBuilder()
					.SetPropertyName("asdf")
					.SetDeclaringType<object>()
					.Build();
			});

			Assert.Throws<ArgumentNullException>(buildAction);
		}

		[Fact]
		public void BuildShowThrowAnExceptionIfBindablePropertyDeclaringTypeIsNull()
		{
			var buildAction = new Action(() =>
			{
				_ = new BindablePropertyBuilder()
					.SetPropertyName("asdf")
					.SetReturnType<object>()
					.SetDeclaringType(null)
					.Build();
			});

			Assert.Throws<ArgumentNullException>(buildAction);
		}

		[Fact]
		public void BuildShouldThrowAnExceptionIfSetDeclaringTypeGenericIsNotCalled()
		{
			var buildAction = new Action(() =>
			{
				_ = new BindablePropertyBuilder()
					.SetPropertyName("asdf")
					.SetReturnType<object>()
					.Build();
			});

			Assert.Throws<ArgumentNullException>(buildAction);
		}

		[Fact]
		public void BuildShouldThrowAnExceptionIfBindablePropertyReturnTypeAndDefaultValueTypesAreMismatched()
		{
			var buildAction = new Action(() =>
			{
				_ = new BindablePropertyBuilder()
					.SetPropertyName("asdf")
					.SetDeclaringType<object>()
					.SetReturnType<DateTime>()
					.SetDefaultValue(123)
					.Build();
			});

			Assert.Throws<ArgumentException>(buildAction);
		}

		[Fact]
		public void CallingBuildMultipleTimesShouldCreateNewInstanceOfBindablePropertyParameters()
		{
			var bp1 = new BindablePropertyBuilder()
				.SetPropertyName("asdf")
				.SetDeclaringType<object>()
				.SetReturnType<Guid>()
				.SetDefaultValue(Guid.NewGuid())
				.Build();

			// purposely skipping SetDefaultValue on bp2 to ensure the builder
			// doesn't use bp1's default value
			var bp2 = new BindablePropertyBuilder()
				.SetPropertyName("qwerty")
				.SetDeclaringType<object>()
				.SetReturnType<Guid>()
				.Build();

			Assert.NotEqual(bp1.DefaultValue, bp2.DefaultValue);
		}

		/// <summary>
		/// This control exists solely for testing the various delegates of <see cref="BindableProperty"/>
		/// as we can't manually invoke them on an instance of <see cref="BindableProperty"/>
		/// </summary>
		class TestControl : View
		{
			public BindableProperty TestValueProperty { get; set; }

			public int TestValue
			{
				get => (int)GetValue(TestValueProperty);
				set => SetValue(TestValueProperty, value);
			}
		}
	}
}
