namespace Edsa.AutoCadProxy
{
    public class DrawResult
    {
        public DrawResult()
        {
            this.ObjectId = 0;
            this.Status = DrawStatus.NotFinished;
        }

        public int ObjectId { get; set; }
        public DrawStatus Status { get; set; }
    }

    public enum DrawStatus
    {
        /// <summary>ユーザーの操作が完了していない場合【初期値】</summary>
        NotFinished,
        /// <summary>作図せずにEscを押した場合</summary>
        Canceled,
        /// <summary>作図してEscを押した場合</summary>
        DrawnAndCanceled,
        /// <summary>作図してEnterを押した場合</summary>
        Drawn,
        /// <summary>作図した図形の情報を取得できなかった場合</summary>
        Failed
    }
}
