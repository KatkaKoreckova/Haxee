using Haxee.Entities.DTOs;
using Haxee.MQTTConsumer.Services;

namespace Haxee.Test
{
    public class HelperServiceTest
    {
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