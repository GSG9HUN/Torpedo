using Microsoft.VisualStudio.TestTools.UnitTesting;
using Torpedo.Modell.Single_modell;
namespace TorpedoTest
{
    [TestClass]
    public class GamemodellTest
    {
        Gamemodell game = new Gamemodell();
        [TestMethod]
        public void get_random_irany_test()
        {
           
            Assert.AreEqual("fel", game.get_random_irany(1));
           
            Assert.AreEqual("le", game.get_random_irany(2));
            
            Assert.AreEqual("bal", game.get_random_irany(4));
            
            Assert.AreEqual("jobb", game.get_random_irany(3));
            
            Assert.AreEqual("", game.get_random_irany(0));
            
            Assert.AreEqual("", game.get_random_irany(1456498));
        }
        [TestMethod]
        public void ship_ending_tests()
        {
            Assert.AreEqual(79, game.ship_ending("fel", 99, 3));
           
            Assert.AreEqual(20, game.ship_ending("le", 0, 3));
            
            Assert.AreEqual(92, game.ship_ending("jobb", 90, 3));
            
            Assert.AreEqual(97, game.ship_ending("bal", 99, 3));

        }


        [TestMethod]
        public void check_if_i_have_grid_next_left_tests()
        {
            Assert.AreEqual(false, game.check_if_i_have_grid_next_left(0));
           
            Assert.AreEqual(true, game.check_if_i_have_grid_next_left(88));
            
            Assert.AreEqual(true, game.check_if_i_have_grid_next_left(15));
            
            Assert.AreEqual(true, game.check_if_i_have_grid_next_left(98));
            
            Assert.AreEqual(false, game.check_if_i_have_grid_next_left(10));

        }

        [TestMethod]
        public void check_if_i_have_grid_next_right_tests()
        {
            Assert.AreEqual(false, game.check_if_i_have_grid_next_right(89));
           
            Assert.AreEqual(true, game.check_if_i_have_grid_next_right(88));
            
            Assert.AreEqual(true, game.check_if_i_have_grid_next_right(15));
            
            Assert.AreEqual(true, game.check_if_i_have_grid_next_right(98));
            
            Assert.AreEqual(false, game.check_if_i_have_grid_next_right(29));


        }

        [TestMethod]
        public void check_if_i_have_grid_next_above_tests()
        {
            Assert.AreEqual(false, game.check_if_i_have_grid_above(9));
           
            Assert.AreEqual(true, game.check_if_i_have_grid_above(88));
            
            Assert.AreEqual(true, game.check_if_i_have_grid_above(15));
            
            Assert.AreEqual(true, game.check_if_i_have_grid_above(98));
            
            Assert.AreEqual(false, game.check_if_i_have_grid_above(5));
        }
        [TestMethod]
        public void check_if_i_have_grid_next_under_tests()
        {
            Assert.AreEqual(false, game.check_if_i_have_grid_under(92));
           
            Assert.AreEqual(true, game.check_if_i_have_grid_under(88));
            
            Assert.AreEqual(true, game.check_if_i_have_grid_under(15));
            
            Assert.AreEqual(true, game.check_if_i_have_grid_under(0));
            
            Assert.AreEqual(false, game.check_if_i_have_grid_under(90));
        }

        [TestMethod]
        public void check_shi_tests()
        {
            Assert.AreEqual(0, game.check_ship("asfsgfsd"));

            Assert.AreEqual(5, game.check_ship("carrier"));
            
            Assert.AreEqual(2, game.check_ship("destroyer"));
            
            Assert.AreEqual(3, game.check_ship("submarine"));
            
            Assert.AreEqual(3, game.check_ship("cruiser"));
            
            Assert.AreEqual(4, game.check_ship("battleship"));
            
            Assert.AreEqual(0, game.check_ship(""));


        }
    }

}
