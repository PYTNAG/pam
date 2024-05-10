using System;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using System.Linq;

namespace PAM.Core.App.Residents.Queries
{
    public class GetPersonalAccountsListQuery
    {
        public int PageCapacity { get; set; }
        public int Page { get; set; }

        public string Number { get; set; }
        public DateTime? OpenAt { get; set; }
        public bool WithResidents { get; set; }
        public string Address { get; set; }
        
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronym { get; set; }

        public bool IsNull =>
            Number is null
            && OpenAt is null
            && !WithResidents
            && Address is null
            && FirstName is null
            && SecondName is null
            && Patronym is null;

        public GetPersonalAccountsListQuery(IEnumerable<KeyValuePair<string, StringValues>> parameters)
        {
            var parametersArray = 
                parameters as KeyValuePair<string, StringValues>[] ?? parameters.ToArray();

            try
            {
                PageCapacity = int.Parse(parametersArray.First(p => p.Key == "capacity").Value[0]);
                Page = int.Parse(parametersArray.First(p => p.Key == "page").Value[0]);
            }
            catch (InvalidOperationException)
            {
                throw new Exception($"Missing required parameters: page, capacity.");
            }

            if (parametersArray.Length == 2)
            {
                return;
            }

            foreach (var parameter in parameters)
            {
                switch (parameter.Key)
                {
                    case "number":
                        Number = parameter.Value[0];
                        return; // if number is specified then the other filters have no meaning
                    
                    case "open_at":
                        OpenAt = DateTime.Parse(parameter.Value[0]);
                        break;

                    case "with_residents":
                        WithResidents = true; 
                        break;

                    case "with_address":
                        Address = parameter.Value[0];
                        break;

                    case "firstname":
                        FirstName = parameter.Value[0];
                        break;

                    case "secondname":
                        SecondName = parameter.Value[0];
                        break;

                    case "patronym":
                        Patronym = parameter.Value[0];
                        break;
                }
            }
        }
    }
}