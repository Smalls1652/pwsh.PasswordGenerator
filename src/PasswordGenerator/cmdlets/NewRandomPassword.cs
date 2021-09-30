using System.Collections.Generic;
using System.Management.Automation;
using System.Security;

namespace PasswordGenerator.Cmdlets
{
    using Helpers;

    /// <summary>
    /// PowerShell cmdlet for generating a random password.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "RandomPassword")]
    public class NewRandomPassword : PSCmdlet
    {
        /// <summary>
        /// The length the password should be.
        /// </summary>
        [Parameter(Position = 0)]
        public int PasswordLength
        {
            get => _passwordLength;
            set => _passwordLength = value;
        }
        private int _passwordLength = 14;

        /// <summary>
        /// An array of characters to be ignored from the generated password.
        /// </summary>
        [Parameter(Position = 1)]
        public List<char> IgnoredCharacters { get; set; }

        /// <summary>
        /// A switch for forcing the output to be string type.
        /// </summary>
        [Parameter(Position = 2)]
        public SwitchParameter AsString { get; set; }

        [Parameter(Position = 3)]
        public SwitchParameter Shuffle
        {
            get => _shuffle;
            set => _shuffle = value;
        }
        private SwitchParameter _shuffle = false;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            WriteVerbose("Generating password.");

            if (_shuffle)
            {
                WriteWarning("Shuffling will take longer to execute.");
            }

            if (AsString)
            {
                WriteWarning("Password is being output as a string.");
                WriteObject(Generator.CreatePassword_String(_passwordLength, IgnoredCharacters, _shuffle));
            }
            else
            {
                WriteObject(Generator.CreatePassword_SecureString(_passwordLength, IgnoredCharacters, _shuffle));
            }
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
        }
    }
}