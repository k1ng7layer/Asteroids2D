using MyUISystem;

namespace AsteroidsECS
{
    public class RestartGameButton : UIButton
    {
        protected override void OnClick()
        {
            Game.LoadScene(0);
        }
    }
}
