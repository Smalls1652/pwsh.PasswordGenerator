using System.Collections.Generic;
using System.Management.Automation;

namespace PasswordGenerator.Cmdlets
{
    using Helpers;

    [Cmdlet(VerbsCommon.New, "RandomPassword")]
    public class NewRandomPassword : PSCmdlet
    {

        [Parameter(Position = 0)]
        public int PasswordLength
        {
            get
            {
                return _passwordLength;
            }

            set
            {
                _passwordLength = value;
            }
        }

        [Parameter(Position = 1)]
        public List<string> IgnoredCharacters
        {
            get
            {
                return _ignoredCharacters;
            }

            set
            {
                _ignoredCharacters = value;
            }
        }

        private int _passwordLength = 12;
        private List<string> _ignoredCharacters;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            WriteVerbose("Generating password.");
            string generatedPassword = Generator.CreatePassword(_passwordLength, _ignoredCharacters);

            WriteObject(generatedPassword);
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
        }
    }
}