using Haxee.Entities.DTOs;
using Haxee.MQTTConsumer.Services;

namespace Haxee.Test
{
    public class HelperServiceTest
    {
        [Test]
        public void ValidateIpValidAddress()
        {
            Assert.IsTrue(HelperService.ValidateIp("1.23.123.45"));
        }

        [Test]
        public void ValidateIpInvalidAddress()
        {
            Assert.IsFalse(HelperService.ValidateIp("1"));
            Assert.IsFalse(HelperService.ValidateIp("1,23,45,67"));
            Assert.IsFalse(HelperService.ValidateIp("123.234.456.7"));
            Assert.IsFalse(HelperService.ValidateIp("1.2.3.."));
            Assert.IsFalse(HelperService.ValidateIp("1.2.3.a."));
        }

        [Test]
        public void ValidateYearValidYear()
        {
            Assert.IsTrue(HelperService.ValidateYear("2023"));
        }

        [Test]
        public void ValidateYearInvalidYear()
        {
            Assert.IsFalse(HelperService.ValidateYear("2023.1"));
            Assert.IsFalse(HelperService.ValidateYear("aa"));
        }

        [Test]
        public void ValidateMenuOptionValidOption()
        {
            Assert.IsTrue(HelperService.ValidMenuOption(new List<int>(){ 1, 2, 4, 8, 5}, 4));
        }

        [Test]
        public void ValidateMenuOptionInvalidOption()
        {
            Assert.IsFalse(HelperService.ValidMenuOption(new List<int>() { 1, 2, 4, 8, 5 }, 3));
        }

        [Test]
        public void ValidatePortValidOption()
        {
            Assert.IsTrue(HelperService.ValidatePort("1883"));
        }

        [Test]
        public void ValidatePortInvalidOption()
        {
            Assert.IsFalse(HelperService.ValidatePort("a"));
            Assert.IsFalse(HelperService.ValidatePort("123456"));
        }

        [Test]
        public void ValidateTopicValidText()
        {
            Assert.IsTrue(HelperService.ValidateTopic("topic/#"));
        }

        [Test]
        public void ValidateTopicInvalidText()
        {
            Assert.IsFalse(HelperService.ValidateTopic("/topic/#"));
            Assert.IsFalse(HelperService.ValidateTopic("/topic"));
            Assert.IsFalse(HelperService.ValidateTopic("topic/"));
            Assert.IsFalse(HelperService.ValidateTopic("topic#"));
            Assert.IsFalse(HelperService.ValidateTopic("topic#/"));
            Assert.IsFalse(HelperService.ValidateTopic("topic/#/#"));
            Assert.IsFalse(HelperService.ValidateTopic("topic/+/#"));
            Assert.IsFalse(HelperService.ValidateTopic("$topic/#"));
            Assert.IsFalse(HelperService.ValidateTopic("topic>/#"));
            Assert.IsFalse(HelperService.ValidateTopic("topic*/#"));
            Assert.IsFalse(HelperService.ValidateTopic("topic//#"));
            Assert.IsFalse(HelperService.ValidateTopic("topic\\/#"));
            Assert.IsFalse(HelperService.ValidateTopic("/#"));
            Assert.IsFalse(HelperService.ValidateTopic("#"));
            Assert.IsFalse(HelperService.ValidateTopic(""));
        }

        [Test]
        public void ValidateCurrentYearSetupValid()
        {
            Assert.IsTrue(HelperService.ValidateCurrentYearSetup("2023", "mytopic/#", "1883", "127.0.0.1"));
        }

        [Test]
        public void ValidateCurrentYearSetupInvalid()
        {
            Assert.IsFalse(HelperService.ValidateCurrentYearSetup("2023", "mytopic", "1883", "127.0.0.1"));
        }

        [Test]
        public void ValidateMessageValid()
        {
            AttendeeInformationDTO? attendeeInformation = HelperService.ParseMessage("1 12.1.2023 10:50 1", "my/topic");
            if (attendeeInformation is not null)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void ValidateMessageInvalid()
        {
            AttendeeInformationDTO? attendeeInformation = HelperService.ParseMessage("1 12.1 10:50 1", "my/topic");
            if (attendeeInformation is null)
                Assert.Pass();
            else
                Assert.Fail();
        }
    }
}