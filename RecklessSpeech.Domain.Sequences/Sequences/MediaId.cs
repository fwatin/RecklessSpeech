namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public class Media
    {
        public long MediaId { get; set; }
        public string? LeftImage { get; set; }
        public string? RightImage { get; set; }
        public string? Mp3 { get; set; }

        public bool IsComplete()
        {
            return this.LeftImage is not null && this.RightImage is not null && this.Mp3 is not null;
        }

        private Media() { }

        public static Media Create(long mediaId, string? leftImage, string? rightImage, string? mp3)
        {
            return new()
            {
                MediaId = mediaId,
                Mp3 = mp3,
                LeftImage = leftImage,
                RightImage = rightImage
            };
        }
    }
}