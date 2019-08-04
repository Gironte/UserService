using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UserInformation.WebService.Models;


namespace UserInformation.WebService.App_Start
{
    public class AccountRequestConverterConfig : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(MyAccountRequestBase);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);
            MyAccountRequestBase accountRequestBase =  new SyncProfileRequest();
            serializer.Populate(obj.CreateReader(), accountRequestBase);
            return accountRequestBase;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) 
        {
            throw new NotImplementedException();
        }
    }
}
