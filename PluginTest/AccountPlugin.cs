using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace PluginTest
{
    public class AccountPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IPluginExecutionContext execContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationService orgService = serviceFactory.CreateOrganizationService(execContext.UserId);

            //This is a change 1
            //This is change 2
            // This is from GitHub
            //from Laptop 1
            //Now again from my PC

            //Now vivek from company laptop

            //Again PC 2

            // Laptop - restarting VS 

            // Branch 1

            Entity entity = (Entity)execContext.InputParameters["Target"];

            if (entity.Contains("telephone1"))
            {

                if (execContext.Depth > 1)
                {
                    return;
                }

                string accountPhone = entity.GetAttributeValue<string>("telephone1");

                QueryExpression query = new QueryExpression("contact");
                query.ColumnSet = new ColumnSet(false);
                query.Criteria.AddCondition(new ConditionExpression("parentcustomerid", ConditionOperator.Equal, entity.Id));
                query.NoLock = true;

                var results = orgService.RetrieveMultiple(query);

                foreach (var e in results.Entities)
                {
                    e["telephone1"] = accountPhone;
                    orgService.Update(e);
                }
            }

            entity["description"] = "Last Update at " + DateTime.Now.ToLongDateString();
            orgService.Update(entity);
        }
    }
}
