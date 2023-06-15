﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Json.Schema;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;

namespace Microsoft.OpenApi.Readers.Extensions
{
    internal static class JsonSchemaBuilderExtensions
    {
        public static JsonSchemaBuilder Extensions(this JsonSchemaBuilder builder, IDictionary<string, IOpenApiExtension> extensions)
        {
            builder.Add(new ExtensionsKeyword(extensions));
            return builder;
        }
        public static JsonSchemaBuilder AdditionalPropertiesAllowed(this JsonSchemaBuilder builder, bool additionalPropertiesAllowed)
        {
            builder.Add(new AdditionalPropertiesAllowedKeyword(additionalPropertiesAllowed));
            return builder;
        }

        public static JsonSchemaBuilder Nullable(this JsonSchemaBuilder builder, bool value)
        {
            builder.Add(new NullableKeyword(value));
            return builder;
        }

        public static JsonSchemaBuilder ExclusiveMaximum(this JsonSchemaBuilder builder, bool value)
        {
            builder.Add(new Draft4ExclusiveMaximumKeyword(value));
            return builder;
        }

        public static JsonSchemaBuilder ExclusiveMinimum(this JsonSchemaBuilder builder, bool value)
        {
            builder.Add(new Draft4ExclusiveMinimumKeyword(value));
            return builder;
        }
    }

    [SchemaKeyword(Name)]
    internal class Draft4ExclusiveMinimumKeyword : IJsonSchemaKeyword
    {
        public const string Name = "exclusiveMinimum";

        /// <summary>
        /// The ID.
        /// </summary>
        public bool MinValue { get; }

        internal Draft4ExclusiveMinimumKeyword(bool value)
        {
            MinValue = value;
        }

        // Implementation of IJsonSchemaKeyword interface
        public void Evaluate(EvaluationContext context)
        {
            throw new NotImplementedException();
        }
    }

    [SchemaKeyword(Name)]
    internal class Draft4ExclusiveMaximumKeyword : IJsonSchemaKeyword
    {
        public const string Name = "exclusiveMaximum";

        /// <summary>
        /// The ID.
        /// </summary>
        public bool MaxValue { get; }

        internal Draft4ExclusiveMaximumKeyword(bool value)
        {
            MaxValue = value;
        }

        // Implementation of IJsonSchemaKeyword interface
        public void Evaluate(EvaluationContext context)
        {
            throw new NotImplementedException();
        }
    }

    internal class NullableKeyword : IJsonSchemaKeyword
    {
        public const string Name = "nullable";

        /// <summary>
        /// The ID.
        /// </summary>
        public bool Value { get; }

        /// <summary>
        /// Creates a new <see cref="IdKeyword"/>.
        /// </summary>
        /// <param name="value">Whether the `minimum` value should be considered exclusive.</param>
        public NullableKeyword(bool value)
        {
            Value = value;
        }

        public void Evaluate(EvaluationContext context)
        {
            context.EnterKeyword(Name);
            var schemaValueType = context.LocalInstance.GetSchemaValueType();
            if (schemaValueType == SchemaValueType.Null && !Value)
            {
                context.LocalResult.Fail(Name, "nulls are not allowed"); // TODO: localize error message
            }
            context.ExitKeyword(Name, context.LocalResult.IsValid);
        }
    }

    [SchemaKeyword(Name)]
    internal class ExtensionsKeyword : IJsonSchemaKeyword
    {
        public const string Name = "extensions";

        internal IDictionary<string, IOpenApiExtension> Extensions { get; }

        internal ExtensionsKeyword(IDictionary<string, IOpenApiExtension> extensions)
        {
            Extensions = extensions;
        }

        // Implementation of IJsonSchemaKeyword interface
        public void Evaluate(EvaluationContext context)
        {
            throw new NotImplementedException();
        }
    }

    internal class AdditionalPropertiesAllowedKeyword : IJsonSchemaKeyword
    {
        internal bool AdditionalPropertiesAllowed { get; }

        internal AdditionalPropertiesAllowedKeyword(bool additionalPropertiesAllowed)
        {
            AdditionalPropertiesAllowed = additionalPropertiesAllowed;
        }

        // Implementation of IJsonSchemaKeyword interface
        public void Evaluate(EvaluationContext context)
        {
            throw new NotImplementedException();
        }
    }
}