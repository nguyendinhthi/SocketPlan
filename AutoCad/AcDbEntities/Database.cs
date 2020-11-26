
namespace Edsa.AutoCadProxy
{
    public class Database
    {
        public int GetLayoutDictionary()
        {
            var result = this.DoDB<int>("getLayoutDictionary");

            if (!result.Success)
                throw new AutoCadException("Failed to get layout.");

            return result.Value;
        }

        public int GetGroupDictionary()
        {
            var result = this.DoDB<int>("getGroupDictionary");

            if (!result.Success)
                throw new AutoCadException("Failed to get group.");

            return result.Value;
        }

        public int GetPaperSpaceViewportId()
        {
            var result = this.DoDB<int>("ペーパー空間ビューポートID取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get paper space viewport id.");

            return result.Value;
        }

        public int GetCurrentSpaceId()
        {
            var result = this.DoDB<int>("現行空間ID取得");
            if (!result.Success)
                throw new AutoCadException("Failed to get current space id.");

            return result.Value;
        }

        public bool IsOrthogonalMode()
        {
            var result = this.DoDB<short>("Orthomode");
            if (!result.Success)
                throw new AutoCadException("Failed to get orthogonal mode.");

            return result.Value == 1;
        }

        public int GetOSnapMode()
        {
            var result = this.DoDB<short>("OSMODE");
            if (!result.Success)
                throw new AutoCadException("Failed to get orthogonal mode.");

            return result.Value;
        }

        public void SetOrthogonalMode(bool enable)
        {
            AutoCad.Command.SendLineEsc("Ortho " + (enable ? 1 : 0));
        }

        public void SetOSnapMode(int oSnap)
        {
            AutoCad.Command.SendLineEsc("OSMODE " + oSnap);
        }

        public void SetOSnapModeFull()
        {
            this.SetOSnapMode(3071);
        }

        public void SetFileDialogMode(bool enable)
        {
            AutoCad.Command.SendLineEsc("FILEDIA " + (enable ? 1 : 0));
        }

        public void SetLineWeightDisplay(bool enable)
        {
            AutoCad.Command.SendLineEsc("LWDISPLAY " + (enable ? "ON" : "OFF"));
        }

        protected Result<G> DoDB<G>(string method)
        {
            object setData = null;
            object getData = null;
            bool isSucceeded = AutoCad.vbcom.Db(method, ref setData, ref getData);

            if (getData == null)
                return new Result<G>(isSucceeded, default(G));

            return new Result<G>(isSucceeded, (G)getData);
        }
    }
}
