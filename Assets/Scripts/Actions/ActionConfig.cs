namespace MyActionContainer
{
    public static class ActionConfig
    {
        private static bool _isConfigured;
        public static void ConfigureActions()
        {
            if (!_isConfigured)
            {
                ActionContainer.AddAction<GameOverAction>();
                ActionContainer.AddAction<StartGameAction>();
                //UIActionContainer.AddAction<SwitchUIStateAction>();
            }
            _isConfigured = true;
        }
    }
}
