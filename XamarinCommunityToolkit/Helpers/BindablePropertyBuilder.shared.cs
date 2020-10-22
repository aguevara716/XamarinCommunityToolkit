using System;
using Xamarin.Forms;

namespace Xamarin.CommunityToolkit.Helpers
{
	public class BindablePropertyBuilder
	{
		Type declaringType;
		BindingMode defaultBindingMode;
		object defaultValue;
		string propertyName;
		Type returnType;

		BindableProperty.BindingPropertyChangedDelegate bindingPropertyChangedDelegate;
		BindableProperty.BindingPropertyChangingDelegate bindingPropertyChangingDelegate;
		BindableProperty.CoerceValueDelegate coerceValueDelegate;
		BindableProperty.ValidateValueDelegate validateValueDelegate;
		BindableProperty.CreateDefaultValueDelegate createDefaultValueDelegate;

		public BindablePropertyBuilder()
        {
			declaringType = null;
			defaultBindingMode = BindingMode.OneWay;
			defaultValue = null;
			propertyName = null;
			returnType = null;

			bindingPropertyChangedDelegate = null;
			bindingPropertyChangingDelegate = null;
			coerceValueDelegate = null;
			validateValueDelegate = null;
			createDefaultValueDelegate = null;
        }

		/// <summary>
		/// (Required) Sets the name of the property that is bound to the <see cref="BindableProperty"/>
		/// property that is bound to the <see cref="BindableProperty"/>
		/// </summary>
		/// <param name="propertyName">The name of the property that is bound to the <see cref="BindableProperty"/></param>
		/// <returns>The <see cref="BindablePropertyBuilder"/> to support the fluent syntax</returns>
		public BindablePropertyBuilder SetPropertyName(string propertyName)
		{
			this.propertyName = propertyName;
			return this;
		}

		/// <summary>
		/// (Required) Sets the type of the value that is bound to the <see cref="BindableProperty"/>
		/// </summary>
		/// <param name="returnType">The type of the value that is bound to the <see cref="BindableProperty"/></param>
		/// <returns>The <see cref="BindablePropertyBuilder"/> to support the fluent syntax</returns>
		public BindablePropertyBuilder SetReturnType(Type returnType)
		{
			this.returnType = returnType;
			return this;
		}

		/// <summary>
		/// (Required) Sets the type of the value that is bound to the <see cref="BindableProperty"/>
		/// </summary>
		/// <typeparam name="TReturn">The type of the value that is bound to the <see cref="BindableProperty"/></typeparam>
		/// <returns>The <see cref="BindablePropertyBuilder"/> to suport the fluent syntax</returns>
		public BindablePropertyBuilder SetReturnType<TReturn>() => SetReturnType(typeof(TReturn));

		/// <summary>
		/// Sets the property's default value
		/// </summary>
		/// <param name="defaultValue">The property's default value</param>
		/// <returns>The <see cref="BindablePropertyBuilder"/> to support the fluent syntax</returns>
		public BindablePropertyBuilder SetDefaultValue(object defaultValue)
		{
			this.defaultValue = defaultValue;
			return this;
		}

		/// <summary>
		/// Sets the property's default <see cref="BindingMode"/>
		/// </summary>
		/// <param name="defaultBindingMode">The property's default <see cref="BindingMode"/></param>
		/// <returns>The <see cref="BindablePropertyBuilder"/> to support the fluent syntax</returns>
		public BindablePropertyBuilder SetDefaultBindingMode(BindingMode defaultBindingMode)
		{
			this.defaultBindingMode = defaultBindingMode;
			return this;
		}

		/// <summary>
		/// (Required) Sets the type (usually a control) that's declaring the <see cref="BindableProperty"/>
		/// </summary>
		/// <param name="declaringType">The type (usually a control) that's declaring the <see cref="BindableProperty"/></param>
		/// <returns>The <see cref="BindablePropertyBuilder"/> to support the fluent syntax</returns>
		public BindablePropertyBuilder SetDeclaringType(Type declaringType)
		{
			this.declaringType = declaringType;
			return this;
		}

		/// <summary>
		/// (Required) Sets the type (usually a control) that's declaring the <see cref="BindableProperty"/>
		/// </summary>
		/// <typeparam name="TDeclaring">The type (usually a control) that's declaring the <see cref="BindableProperty"/></typeparam>
		/// <returns>The <see cref="BindablePropertyBuilder"/> to support the fluent syntax</returns>
		public BindablePropertyBuilder SetDeclaringType<TDeclaring>() => SetDeclaringType(typeof(TDeclaring));

		/// <summary>
		/// Sets the delegate that runs when a bound value is set.
		/// </summary>
		/// <param name="validateValueDelegate">The delegate that runs when a bound value is set.</param>
		/// <returns>The <see cref="BindablePropertyBuilder"/> to support the fluent syntax</returns>
		public BindablePropertyBuilder SetValidateValueDelegate(BindableProperty.ValidateValueDelegate validateValueDelegate)
		{
			this.validateValueDelegate = validateValueDelegate;
			return this;
		}

		/// <summary>
		/// Sets the delegate that runs after a bound value has changed.
		/// </summary>
		/// <param name="bindingPropertyChangedDelegate">The delegate that runs after a bound value has changed.</param>
		/// <returns>The <see cref="BindablePropertyBuilder"/> to support the fluent syntax</returns>
		public BindablePropertyBuilder SetPropertyChangedDelegate(BindableProperty.BindingPropertyChangedDelegate bindingPropertyChangedDelegate)
		{
			this.bindingPropertyChangedDelegate = bindingPropertyChangedDelegate;
			return this;
		}

		/// <summary>
		/// Sets the delegate that runs before a bound value is changed.
		/// </summary>
		/// <param name="bindingPropertyChangingDelegate">The delegate that runs before a bound value is changed.</param>
		/// <returns>The <see cref="BindablePropertyBuilder"/> to support the fluent syntax</returns>
		public BindablePropertyBuilder SetPropertyChangingDelegate(BindableProperty.BindingPropertyChangingDelegate bindingPropertyChangingDelegate)
		{
			this.bindingPropertyChangingDelegate = bindingPropertyChangingDelegate;
			return this;
		}

		/// <summary>
		/// Sets the delegate used to coerce the range of a value.
		/// </summary>
		/// <param name="coerceValueDelegate">A delegate used to coerce the range of a value.</param>
		/// <returns>The <see cref="BindablePropertyBuilder"/> to support the fluent syntax</returns>
		public BindablePropertyBuilder SetCoerceValueDelegate(BindableProperty.CoerceValueDelegate coerceValueDelegate)
		{
			this.coerceValueDelegate = coerceValueDelegate;
			return this;
		}

		/// <summary>
		/// Sets the delegate used to initialize the default value for reference types.
		/// </summary>
		/// <param name="createDefaultValueDelegate">The delegate used to initialize the default value for reference types.</param>
		/// <returns>The <see cref="BindablePropertyBuilder"/> to support the fluent syntax</returns>
		public BindablePropertyBuilder SetCreateDefaultValueDelegate(BindableProperty.CreateDefaultValueDelegate createDefaultValueDelegate)
		{
			this.createDefaultValueDelegate = createDefaultValueDelegate;
			return this;
		}

		public BindableProperty Build()
        {
			if (string.IsNullOrEmpty(propertyName))
				throw new ArgumentNullException(nameof(propertyName), $"{nameof(propertyName)} is required");
			if (returnType == default)
				throw new ArgumentNullException(nameof(returnType), $"{nameof(returnType)} is required");
			if (declaringType == default)
				throw new ArgumentNullException(nameof(declaringType), $"{nameof(declaringType)} is required");

			var bp = BindableProperty.Create(propertyName: propertyName,
											 returnType: returnType,
											 declaringType: declaringType,
											 defaultValue: defaultValue,
											 defaultBindingMode: defaultBindingMode,
											 validateValue: validateValueDelegate,
											 propertyChanged: bindingPropertyChangedDelegate,
											 propertyChanging: bindingPropertyChangingDelegate,
											 coerceValue: coerceValueDelegate,
											 defaultValueCreator: createDefaultValueDelegate);
			return bp;
		}
	}
}
