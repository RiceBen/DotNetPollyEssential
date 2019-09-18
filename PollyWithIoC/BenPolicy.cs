using Polly;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace PollyWithIoC
{
    public class BenPolicy : IBenPolicy
    {
        public void ExecuteWithPolicy(Action action, IDictionary<string, object> customContext)
        {
            this.GetPolicy().Execute((ctx) =>
            {
                action();
            }, customContext);
        }

        public ISyncPolicy GetPolicy()
        {
            var myContext = new JObject();
            myContext.Add("Prop_1", "Value_1");

            ISyncPolicy redo1 = Policy.Handle<ApplicationException>()
                                      .Retry(1,
                                      (exception, count, context) =>
                                      {
                                          var dic = context as IDictionary<string, object>;
                                          myContext["anotherNode"] = JObject.FromObject(dic);
                                          Console.WriteLine($"Execute by Polly ApplicationException, exception:{exception.Message}, count:{count}, context:{JsonConvert.SerializeObject(myContext)}");
                                      });

            ISyncPolicy redo2 = Policy.Handle<NullReferenceException>()
                                      .Retry(1,
                                      (exception, count, context) =>
                                      {
                                          var dic = context as IDictionary<string, object>;
                                          myContext["anotherNode"] = JObject.FromObject(dic);
                                          Console.WriteLine($"Execute by Polly NullReferenceException, exception:{exception.Message}, count:{count}, context:{JsonConvert.SerializeObject(myContext)}");
                                      });

            ISyncPolicy redo3 = Policy.Handle<InvalidOperationException>()
                                      .Retry(1,
                                      (exception, count, context) =>
                                      {
                                          var dic = context as IDictionary<string, object>;
                                          myContext["anotherNode"] = JObject.FromObject(dic);
                                          Console.WriteLine($"Execute by Polly InvalidOperationException, exception:{exception.Message}, count:{count}, context:{JsonConvert.SerializeObject(myContext)}");
                                      });

            ISyncPolicy myPolicyWrap =
            Policy.Wrap(redo1,
                        redo2,
                        redo3);

            return myPolicyWrap;
        }
    }
}
