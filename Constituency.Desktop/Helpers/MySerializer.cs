

namespace Constituency.Desktop.Helpers
{
    using Constituency.Desktop.Entities;
    using System;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;

    public static class MySerializer
    {
        public static string Serialize(this object obj)
        {
            var serializer = new DataContractJsonSerializer(obj.GetType());
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                return Encoding.Default.GetString(ms.ToArray());
            }
        }

        public static bool VoterEquealtoVpter2(Voter v1, Voter v2)
        {
            if (v1.DOB == v2.DOB && v1.Occupation == v2.Occupation && v1.Address == v2.Address && v1.Email==v2.Email && v1.FullName == v2.FullName && v1.HomePhone==v2.HomePhone && v1.Mobile1==v2.Mobile1 && v1.Mobile2 == v2.Mobile2 && v1.WorkPhone == v2.WorkPhone && v1.PollingDivision.Id== v2.PollingDivision.Id && v1.Reg == v2.Reg && v1.Sex==v2.Sex && v1.Dead==v2.Dead)
            {
                return true;
            }
            return false;
        }
    }
}
