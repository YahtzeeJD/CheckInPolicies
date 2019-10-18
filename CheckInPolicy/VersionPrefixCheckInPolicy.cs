using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Linq;

namespace CheckInPolicies
{
    [Serializable()]
    public class VersionPrefixCheckInPolicy : PolicyBase
    {
        public override string Type => "Custom Checkin Policy";
        public override string TypeDescription => "This policy will prompt the user to remember to update the version-prefix.txt file using semantic versioning before checking in.";
        public override string Description => "Reminding developers to update version-prefix.txt since 2019...  ";

        public override bool Edit(IPolicyEditArgs policyEditArgs)
        {
            // Do not need any custom configuration
            return true;
        }

        public override PolicyFailure[] Evaluate()
        {
            return PendingCheckin.PendingChanges.AllPendingChanges
                .Any(x => x.FileName.ToLower().Contains("version-prefix.txt"))
                    ? new PolicyFailure[] { }
                    : new PolicyFailure[] { new PolicyFailure("Please update the version-prefix.txt !", this) };
        }
    }
}
