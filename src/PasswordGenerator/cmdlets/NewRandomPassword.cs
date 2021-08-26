using System.Collections.Generic;
using System.Management.Automation;

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
        public int PasswordLength { get; set; }

        /// <summary>
        /// An array of characters to be ignored from the generated password.
        /// </summary>
        [Parameter(Position = 1)]
        public List<string> IgnoredCharacters { get; set; }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }
        
        protected override void ProcessRecord()
        {
            WriteVerbose("Generating password.");
            string generatedPassword = Generator.CreatePassword(PasswordLength, IgnoredCharacters);

            WriteObject(generatedPassword);
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
        }
    }
}