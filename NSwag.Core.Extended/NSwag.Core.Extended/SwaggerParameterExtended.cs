﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NSwag
{
    public class SwaggerParameterExtended : SwaggerParameter
    {
        /// <summary>Gets or sets the kind of the parameter.</summary>
        [JsonProperty(PropertyName = "in", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public new SwaggerParameterKindExtended Kind { get; set; }
    }
}
