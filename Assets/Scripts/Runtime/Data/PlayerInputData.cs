namespace AsteroidsECS
{
    public class PlayerInputData
    {
        public Controlls InputActions { get; set; }
        public PlayerInputData(Controlls inputActions)
        {
            InputActions = inputActions;
        }
    }
}
