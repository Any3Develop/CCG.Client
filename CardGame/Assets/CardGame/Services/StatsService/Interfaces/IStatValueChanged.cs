using System;

namespace CardGame.Services.StatsService
{
    public interface IStatValueChanged
    {
        event EventHandler<float> OnValueChanged;
    }
}