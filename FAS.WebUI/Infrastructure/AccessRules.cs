using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FAS.WebUI.Infrastructure
{
    public static class AccessRules
    {
        private static Dictionary<Target, Dictionary<Permission, IEnumerable<Claim>>> rules;

        static AccessRules()
        {
            rules.Add(Target.Account, getRulesForAccount());
            rules.Add(Target.Home, getRulesForHome());
        }

        public static bool HasAccess(this IEnumerable<Claim> claims, Target target, Permission permission)
        {
            var _claims = claims.Where(claim => claim.Type.Equals(target.ToString())).ToArray();

            return target.getClaimsForTargetByPermission(permission)
                    .All(claim => _claims.Any(uc => uc.Type.Equals(claim.Type) &&
                        (uc.Value.Equals(claim.Value) || uc.Value.CompareTo(claim.Value) == 1)));
        }

        private static IEnumerable<Claim> getClaimsForTargetByPermission(this Target target, Permission permission)
        {
            IEnumerable<Claim> result = new Claim[0];

            foreach (var perm in new[] { Permission.Create, Permission.Delete, Permission.Read, Permission.Update })
            {
                if ((permission & perm) == perm)
                {
                    result = result.Concat(rules[target][perm]);
                }
            }

            return result;
        }

        private static Dictionary<Permission, IEnumerable<Claim>> getRulesForAccount()
        {
            return new Dictionary<Permission, IEnumerable<Claim>>
            {
                {Permission.Create, new Claim[0] },
                {Permission.Read, new Claim[0] },
                {Permission.Update, new Claim[0] },
                {Permission.Delete, new [] { new Claim(ClaimTypes.Role, "Admin") } },
            };
        }

        private static Dictionary<Permission, IEnumerable<Claim>> getRulesForHome()
        {
            return new Dictionary<Permission, IEnumerable<Claim>>
            {
                {Permission.Create, new Claim[0] },
                {Permission.Read, new Claim[0] },
                {Permission.Update, new Claim[0] },
                {Permission.Delete, new [] { new Claim(ClaimTypes.Role, "Admin"),
                                             new Claim("Age", "18") } }
            };
        }
    }
}