
namespace Racing.Collidables {
	public abstract class Vulture : Hinderance {

		private const float BOOST_STRENGTH = 0.5f;

		public override void applySpecificEffect(Racer racer) {
			racer.adjustSpeed(Vulture.BOOST_STRENGTH);
		}
	}
}