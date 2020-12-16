using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Purchasing;

public class emeraldshopManager : MonoBehaviour, IStoreListener
{

    private IStoreController storeController;
    private IExtensionProvider extensionProvider;


    public const string emerald_10 = "emerald_10";
    public const string emerald_30 = "emerald_30";
    public const string emerald_50 = "emerald_50";
    public const string emerald_100 = "emerald_100";
    public const string emerald_500 = "emerald_500";
    public const string emerald_1000 = "emerald_1000";



    bool dia_gold = false;//false dia , true gold


    string itemName;
    int item_id;

    private bool isInit()
    {
        return (storeController != null && extensionProvider != null);
    }

    public void InitPurchasing()
    {
        if (isInit()) return;

        var module = StandardPurchasingModule.Instance();

        ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);

        builder.AddProduct(emerald_10, ProductType.Consumable, new IDs{
            {emerald_10, GooglePlay.Name}
        });
        builder.AddProduct(emerald_30, ProductType.Consumable, new IDs{
            {emerald_30, GooglePlay.Name}
        });
        builder.AddProduct(emerald_50, ProductType.Consumable, new IDs{
            {emerald_50, GooglePlay.Name}
        });
        builder.AddProduct(emerald_100, ProductType.Consumable, new IDs{
            {emerald_100, GooglePlay.Name}
        });
        builder.AddProduct(emerald_500, ProductType.Consumable, new IDs{
            {emerald_500, GooglePlay.Name}
        });
        builder.AddProduct(emerald_1000, ProductType.Consumable, new IDs{
            {emerald_1000, GooglePlay.Name}
        });
        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyItem(string itemName)
    {
        try
        {
            if (isInit())
            {
                Product p = storeController.products.WithID(itemName);

                if (p != null && p.availableToPurchase)
                {
                    Debug.Log("buy Item : " + p.definition.id);
                    storeController.InitiatePurchase(p);
                }
                else
                {
                    Debug.Log("Fail");
                }
            }
            else
            {
                Debug.Log("Not Init");
            }
        }
        catch (Exception e)
        {
            Debug.Log("Fail " + e);
        }
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        switch (args.purchasedProduct.definition.id)
        {
            case emerald_10:
                GameData.addEmerald(10); GetComponent<CharInfoScript>().ChargeP.SetActive(true);
                break;
            case emerald_30:
                GameData.addEmerald(33); GetComponent<CharInfoScript>().ChargeP.SetActive(true);
                break;
            case emerald_50:
                GameData.addEmerald(58); GetComponent<CharInfoScript>().ChargeP.SetActive(true);
                break;
            case emerald_100:
                GameData.addEmerald(120); GetComponent<CharInfoScript>().ChargeP.SetActive(true);
                break;
            case emerald_500:
                GameData.addEmerald(630); GetComponent<CharInfoScript>().ChargeP.SetActive(true);
                break;
            case emerald_1000:
                GameData.addEmerald(1300); GetComponent<CharInfoScript>().ChargeP.SetActive(true);
                break;
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("FAIL  ", product.definition.storeSpecificId, failureReason));
    }
    public void OnInitialized(IStoreController sc, IExtensionProvider ep)
    {
        storeController = sc;
        extensionProvider = ep;
    }

    public void OnInitializeFailed(InitializationFailureReason reason)
    {
        Debug.Log("Fail Reason : " + reason);
    }
    // Use this for initialization
    void Start()
    {
        InitPurchasing();
    }

    // Update is called once per frame
    void Update()
    {

    }

 
}
