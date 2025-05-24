using System.Security.Cryptography;
using System.Text;

namespace ERP_Service.Application.Services.VNPay;

public class VnPayLibrary
{
    private SortedList<string, string> _requestData = new SortedList<string, string>(new VnPayCompare());
    private SortedList<string, string> _responseData = new SortedList<string, string>(new VnPayCompare());

    public void AddRequestData(string key, string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _requestData.Add(key, value);
        }
    }

    public void AddResponseData(string key, string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _responseData.Add(key, value);
        }
    }

    public string CreateRequestUrl(string baseUrl, string vnp_HashSecret)
    {
        var data = new StringBuilder();
        foreach (var kv in _requestData)
        {
            if (data.Length > 0)
            {
                data.Append("&");
            }
            data.Append(kv.Key).Append("=").Append(Uri.EscapeDataString(kv.Value));
        }

        var queryString = data.ToString();
        var hashData = $"{vnp_HashSecret}{queryString}";
        var vnp_SecureHash = CreateMd5(hashData);

        return $"{baseUrl}?{queryString}&vnp_SecureHash={vnp_SecureHash}";
    }

    public bool ValidateSignature(string inputHash, string vnp_HashSecret)
    {
        var data = new StringBuilder();
        foreach (var kv in _responseData)
        {
            if (kv.Key != "vnp_SecureHash")
            {
                if (data.Length > 0)
                {
                    data.Append("&");
                }
                data.Append(kv.Key).Append("=").Append(Uri.EscapeDataString(kv.Value));
            }
        }

        var queryString = data.ToString();
        var hashData = $"{vnp_HashSecret}{queryString}";
        var vnp_SecureHash = CreateMd5(hashData);

        return vnp_SecureHash.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
    }

    private string CreateMd5(string input)
    {
        using (var md5 = MD5.Create())
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            foreach (var b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
public class VnPayCompare : IComparer<string>
{
    public int Compare(string x, string y)
    {
        return string.CompareOrdinal(x, y);
    }
}