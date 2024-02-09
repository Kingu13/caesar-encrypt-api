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
        [Theory]
        [InlineData("ABC", 3, "ÅÄÖ")]
        [InlineData("HEJ", 3, "EBG")]
        [InlineData("Vad gör du?", 3, "Såa dzo ar?")]
        public void DecryptCaesarTest_ShouldReturnCorrect(string text, int shift, string expected)
        {
            string result = Caesar.CaesarMachine.DecryptCaesar(text, shift);

            Assert.Equal(expected, result);
        }
        [Theory]
        [InlineData("ÅÄÖ", 3, "ABC")]
        [InlineData("TYIO1", 3, "HEJDÅ")]
        [InlineData("SDas eiafj?", 3, "Vad gör du?")]
        public void DecryptCaesarTest_ShouldReturnFalse(string text, int shift, string expected)
        {
            string result = Caesar.CaesarMachine.DecryptCaesar(text, shift);

            Assert.NotEqual(expected, result);
        }
    }
}
