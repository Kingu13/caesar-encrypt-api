using Xunit;

namespace Caesar
{
    public class Tests
    {
        [Theory]
        [InlineData("ABC", 3, "DEF")]
        [InlineData("HEJ", 3, "KHM")]
        [InlineData("Vad gör du?", 3, "Ydg jcu gx?")]
        public void EncryptCaesarTest_ShouldReturnCorrect(string text, int shift, string expected)
        {
            string result = Caesar.CaesarMachine.EncryptCaesar(text, shift);

            Assert.Equal(expected, result);
        }
        [Theory]
        [InlineData("ABC", 3, "ÅÄÖ")]
        [InlineData("HEJDÅ", 3, "TYIO1")]
        [InlineData("Vad gör du?", 3, "SDas eiafj?")]
        public void EncryptCaesarTest_ShouldReturnFalse(string text, int shift, string expected)
        {
            string result = Caesar.CaesarMachine.EncryptCaesar(text, shift);

            Assert.NotEqual(expected, result);
        }
    }
}
