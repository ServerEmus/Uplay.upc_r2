using DllShared;
using System.Text.Json;

namespace upc_r2;

public class UPC_Json
{
    private static readonly string path = Path.Combine(AOTHelper.CurrentPath, "upc.json");
    private static Root? instance;

    public static Root Instance
    {
        get
        {
            if (instance != null)
                return instance;
            if (!File.Exists(path))
            {
                instance = new();
                File.WriteAllText(path, JsonSerializer.Serialize(instance, JsonSourceGen.Default.Root));
                return instance;
            }
            instance = JsonSerializer.Deserialize(File.ReadAllText(path), JsonSourceGen.Default.Root);
            instance ??= new();
            return instance;
        }
    }

    public static void SaveToJson()
    {
        File.WriteAllText(path, JsonSerializer.Serialize(instance, JsonSourceGen.Default.Root));
    }

    public class BasicLog
    {
        public bool ReqLog { get; set; }
        public bool RspLog { get; set; }
        public bool UseNamePipeClient { get; set; }
        public uint WaitBetweebUpdate { get; set; } = 20_000;
        public bool LogUpdate { get; set; }
    }


    public class Account
    {
        public string AccountId { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; } = "user@uplayemu.com";
        public string Name { get; set; } = "user";
        public string Password { get; set; } = "user";
        public string Country { get; set; } = "en-US";
        public string Ticket { get; set; } = string.Empty;
    }

    public class Save
    {
        public string Path { get; set; } = string.Empty;
        public bool UseProductIdInName { get; set; }
        public bool EnableFileDelete { get; set; }
    }

    public class Others
    {
        public string ApplicationId { get; set; } = string.Empty;
        public bool EnableCrossBoot { get; set; }
    }

    public class Product
    {
        public uint ProductId { get; set; }

        // Check Uplay.Uplaydll.ProductType for this.
        // DLC = 2
        // Item = 4
        public uint Type { get; set; }
    }

    public class ChunkIds
    {
        public uint ChunkId { get; set; }
        public string ChunkTag { get; set; } = string.Empty;
    }

    public class Root
    {
        public BasicLog BasicLog { get; set; } = new();
        public Account Account { get; set; } = new();
        public Save Save { get; set; } = new();
        public Others Others { get; set; } = new();
        public List<Product> Products { get; set; } = [new() { ProductId = 0, Type = 4 }];
        public List<uint> AutoProductIds { get; set; } = [];
        public List<ChunkIds> ChunkIds { get; set; } = [];
        public string AvatarsPath { get; set; } = string.Empty;
    }

}
