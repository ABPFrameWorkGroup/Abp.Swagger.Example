﻿/*
 *  感谢yuzukwok  https://github.com/yuzukwok/aspnetboilerplate/tree/master/src/Abp.Web.Api.Swagger
 *  我的ABP Swagger 代码大部分来自该作者
 *  我只是给予了改进
 */
using NJsonSchema;
using NSwag;
using System;

namespace Abp.Swagger
{
    /// <summary>A <see cref="JsonSchemaGenerator"/> which only generate the schema for the root type. 
    /// Referenced types are added to the service's Definitions collection. </summary>
    internal class RootTypeJsonSchemaGenerator : JsonSchemaGenerator
    {
        private bool _isRootType = true;
        private readonly SwaggerServiceExtended _service;

        /// <summary>Initializes a new instance of the <see cref="RootTypeJsonSchemaGenerator" /> class.</summary>
        /// <param name="service">The service.</param>
        /// <param name="settings">The settings.</param>
        public RootTypeJsonSchemaGenerator(SwaggerServiceExtended service, JsonSchemaGeneratorSettings settings)
            : base(settings)
        {
            _service = service;
        }

        /// <summary>Generates the properties for the given type and schema.</summary>
        /// <typeparam name="TSchemaType">The type of the schema type.</typeparam>
        /// <param name="type">The types.</param>
        /// <param name="schema">The properties</param>
        /// <param name="schemaResolver">The schema resolver.</param>
        protected override void GenerateObject<TSchemaType>(Type type, TSchemaType schema, ISchemaResolver schemaResolver)
        {
            if (_isRootType)
            {
                _isRootType = false;
                base.GenerateObject(type, schema, schemaResolver);
            }
            else
            {
                if (!schemaResolver.HasSchema(type))
                {
                    var schemaGenerator = new RootTypeJsonSchemaGenerator(_service, Settings);
                    schemaGenerator.Generate<JsonSchema4>(type, schemaResolver);
                }

                schema.SchemaReference = schemaResolver.GetSchema(type);
            }
        }
    }
}
