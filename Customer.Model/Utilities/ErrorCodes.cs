using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Model.Utilities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ErrorCodes
    {
        //System Error Codes
        [EnumMember(Value = "E001")] AccessTokenInvalid,
        [EnumMember(Value = "E002")] LoginFailed,
        [EnumMember(Value = "E003")] CreateRecordFailed,
        [EnumMember(Value = "E004")] UpdateRecordFailed,
        [EnumMember(Value = "E005")] RecordAlreadyExists,
        [EnumMember(Value = "E006")] InValidRequest,
    }
}
  