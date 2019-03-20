namespace StudyOOP.Convert
{
    // プロパティがない言語ではどうやって、プロパティを実現している？Getのみ
    //// -----------------------------------------------------------
    public class SampleNonPropertyForGet
    {
        private readonly string _fileName;

        public SampleNonPropertyForGet(string fileName)
        {
            this._fileName = fileName;
        }

        public string GetFileName()
        {
            return this._fileName;
        }
    }

    // SampleNonPropertyForGetをプロパティ(自動実装)使用すると以下となります。
    //// -----------------------------------------------------------
    public class SamplePropertyForGet
    {
        private readonly string _fileName; // ←1つのプロパティ作るたびにローカル変数作るのめんどい！！！

        public SamplePropertyForGet(string fileName)
        {
            this._fileName = fileName;
        }

        public string FileName
        {
            get
            {
                return this._fileName;
            }
        }
    }

    // SampleNonPropertyForGetをプロパティ(自動実装)使用すると以下となります。
    //// -----------------------------------------------------------
    public class SampleAutoPropertyForGet
    {
        public SampleAutoPropertyForGet(string fileName)
        {
            this.FileName = fileName;
        }

        public string FileName { get; }
    }

    // プロパティがない言語ではどうやって、プロパティを実現している？GetSet
    //// -----------------------------------------------------------
    public class SampleNonPropertyForGetSet
    {
        private string _fileName;

        public SampleNonPropertyForGetSet(string fileName)
        {
            this._fileName = fileName;
        }

        public string GetFileName()
        {
            return this._fileName;
        }

        internal void SetFileName(string fileName)
        {
            this._fileName = fileName;
        }
    }

    // SampleNonPropertyForGetSetをプロパティ使用すると以下となります。
    //// -----------------------------------------------------------
    public class SamplePropertyForGetSet
    {
        private string _fileName;

        public SamplePropertyForGetSet(string fileName)
        {
            this._fileName = fileName;
        }

        public string FileName
        {
            get
            {
                return this._fileName;
            }

            internal set
            {
                this._fileName = value;
            }
        }
    }

    // SampleNonPropertyForGetSetをプロパティ使用すると以下となります。
    //// -----------------------------------------------------------
    public class SampleAutoPropertyForGetSet
    {
        public SampleAutoPropertyForGetSet(string fileName)
        {
            this.FileName = fileName;
        }

        public string FileName { get; internal set; }
    }

    public static class UsePropertySample
    {
        public static void UseProperty()
        {
            string fileName;

            // プロパティ未使用
            var nonPropertyForGet = new SampleNonPropertyForGet("file");
            fileName = nonPropertyForGet.GetFileName();

            // プロパティ使用
            var propertyForGet = new SamplePropertyForGet("file");
            fileName = propertyForGet.FileName;
            ////propertyForGet.FileName = string.Empty; セッター用意していないためコンパイルエラー

            // プロパティ使用(自動実装プロパティ)
            var autoPropertyForGet = new SampleAutoPropertyForGet("file");
            fileName = autoPropertyForGet.FileName;
            ////autoPropertyForGet.FileName = string.Empty; セッター用意していないためコンパイルエラー

            // プロパティ未使用
            var nonPropertyForGetSet = new SampleNonPropertyForGetSet("file");
            fileName = nonPropertyForGetSet.GetFileName();
            nonPropertyForGetSet.SetFileName(string.Empty); // セッターメソッドが用意されたためセット可能

            // プロパティ使用
            var propertyForGetSet = new SamplePropertyForGetSet("file");
            fileName = propertyForGetSet.FileName;
            propertyForGetSet.FileName = string.Empty; // セッターがあるためセット可能

            // プロパティ使用(自動実装プロパティ)
            var autoPropertyForGetSet = new SampleAutoPropertyForGetSet("file");
            fileName = autoPropertyForGetSet.FileName;
            autoPropertyForGetSet.FileName = string.Empty; // セッターがあるためセット可能
        }
    }
}
