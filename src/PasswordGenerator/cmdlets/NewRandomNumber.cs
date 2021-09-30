using System.Collections.Generic;
using System.Management.Automation;
using System.Security;

namespace PasswordGenerator.Cmdlets
{
    using Helpers;

    [Cmdlet(VerbsCommon.New, "RandomNumber")]
    public class NewRandomNumber : PSCmdlet
    {
        [Parameter(Position = 0)]
        public int MinValue
        {
            get => _minValue;
            set => _minValue = value;
        }
        private int _minValue = 0;

        [Parameter(Position = 1)]
        public int MaxValue
        {
            get => _maxValue;
            set => _maxValue = value;
        }
        private int _maxValue = 100;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            int randomNum = Generator.GetRandomNumber(_minValue, _maxValue);

            WriteObject(randomNum);
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
        }
    }
}