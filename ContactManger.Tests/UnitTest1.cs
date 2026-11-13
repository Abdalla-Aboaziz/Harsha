using ContactsManger.Tests;

namespace ContactManger.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange 
            MyMath myMath = new MyMath();
            int input1 = 5;
            int input2 = 10;

            //Act
            int actual = myMath.Add(input1, input2);
            //Assert
            Assert.Equal(15, actual);

        }
    }
}
