using CommunityToolkit.Mvvm.Messaging.Messages;

namespace WPF_Projekt.Messages
{
    public class SetMainWindowOpacityMessage : ValueChangedMessage<double>
    {
        public SetMainWindowOpacityMessage(double value) : base(value)
        {
        }
    }
}
