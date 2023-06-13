using Dialogue.DTO;

namespace Dialogue
{
    public struct DialogueText
    {
        public string Text { get; private set; }
        public string Person { get; private set; }
        public string MainPersonImage { get; private set; }
        public string SecondPersonImage { get; private set; }
        public string CentralImage { get; private set; }
        public bool MainPersonIsActive { get; private set; }
        public bool SecondPersonIsActive { get; private set; }

        public DialogueText(DialogueTextDTO initDialogueText)
        {
            this.Text = initDialogueText.Text;
            this.Person = initDialogueText.Person;
            this.MainPersonImage = initDialogueText.MainPersonImage;
            this.SecondPersonImage = initDialogueText.SecondPersonImage;
            this.CentralImage = initDialogueText.CentralImage;
            this.MainPersonIsActive = initDialogueText.MainPersonIsActive;
            this.SecondPersonIsActive = initDialogueText.SecondPersonIsActive;
        }
    }
}
