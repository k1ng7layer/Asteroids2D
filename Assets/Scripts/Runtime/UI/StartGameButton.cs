using MyUISystem;

namespace AsteroidsECS
{
    public class StartGameButton : UIButton
    {
        protected override void OnClick()
        {
            Game.LoadScene(1);
        }
    }
}
