using BoardEx.Web.Models.Domain;
using BoardEx.Web.Pages.Admin.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTests
{
    public class AddClassTests
    {

        [Theory]
        [InlineData("ponas vilkas", false)]
        [InlineData("Ponas vilkas", false)]
        [InlineData("ponas Vilkas", false)]
        [InlineData("vilkas", false)]
        [InlineData("Vilkas", false)]
        [InlineData("ponasvilkas", false)]
        [InlineData("Ponas Vilkas", true)]
        [InlineData("Ponas ViLkas", true)]
        public void nameRegexCheck (string name, bool expectation)
        {
            //arrange
            AddModel addModel = new AddModel(null,null);

            //act
            bool result = addModel.nameFormatCheck(name);

            //assert
            Assert.Equal(expectation, result);
        }


        [Theory]
        [InlineData("loremipsum", "loremipsum")]
        [InlineData("lorem ipsum dolor sit amet", "lorem-ipsum-dolor-sit-amet")]
        [InlineData("lorem ipsum   dolor  sit amet", "lorem-ipsum-dolor-sit-amet")]
        [InlineData("lorem     ipsum          dolor     sit  amet", "lorem-ipsum-dolor-sit-amet")]
        public void convertToUrlFormatTest(string text, string expectation)
        {
            //arrange
            AddModel addModel = new AddModel(null, null);

            //act
            string result = addModel.convert(text);

            //assert
            Assert.Equal(expectation, result);
        }





    }
}
