using AutoMapper;
using Customer.BusinessLogic.Interfaces;
using Customer.Data.Application;
using Customer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Customer.BusinessLogic
{
    public class PurchaseOrderManager : BaseManager, IPurchaseOrder
    {
        public PurchaseOrderManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ResponseResults CreatePurchaseOrder(PurchaseOrderModel model)
        {
            var currentUser = GetCurrentUser();
            var mapEntity= Mapper.Map<PurchaseOrder>(model);

            mapEntity.PO_DOE = DateTime.Now;
            mapEntity.PO_Created_By = currentUser.CurrentUserId;
            foreach (var item in mapEntity.PurchaseOrderDetails)
            {
                item.POD_DOE = DateTime.Now;
                item.POD_Created_By = currentUser.CurrentUserId;
                item.IsActive = true;
            }
            _unitOfWork.PurchaseOrder.Insert(mapEntity);
            _unitOfWork.Save();
            return new ResponseResults();
        }

        public ResponseResults DeletePurchaseOrder(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseResults<List<PurchaseOrderModel>> GetPurchaseOrder()
        {
            var result = _unitOfWork.PurchaseOrder.FindAll()
                .Include(x => x.UserDetailsCreatedBy)
                .Include(x => x.UserDetailsUpdatedBy).Where(x => x.IsActive)
                .ToList();
            var mapModel = Mapper.Map<List<PurchaseOrderModel>>(result);
            return new ResponseResults<List<PurchaseOrderModel>>(mapModel);
        }

        public ResponseResults<PurchaseOrderModel> GetPurchaseOrderById(int Id)
        {
            var result = _unitOfWork.PurchaseOrder.FindAll()
               .Include(x => x.UserDetailsCreatedBy)
               .Include(x => x.UserDetailsUpdatedBy)
               .Include(x=>x.PurchaseOrderDetails)
               .FirstOrDefault(x => x.IsActive && x.PurchaseOrderId == Id);
            result.PurchaseOrderDetails = result.PurchaseOrderDetails.Where(x => x.IsActive == true).ToList();
            var mapModel = Mapper.Map<PurchaseOrderModel>(result);
            return new ResponseResults<PurchaseOrderModel>(mapModel);
        }

        public ResponseResults UpdatePurchaseOrder(PurchaseOrderModel model)
        {
            var currentUser = GetCurrentUser();            
            var purchasOrder = _unitOfWork.PurchaseOrder.FindAll()
                .Include(x => x.PurchaseOrderDetails)
                .FirstOrDefault(x => x.PurchaseOrderId == model.PurchaseOrderId);
            purchasOrder.SNo = model.SNo;
            purchasOrder.Date = model.Date;
            purchasOrder.PONo = model.PONo;
            purchasOrder.PODate = model.PODate;
            purchasOrder.GSTNo = model.GSTNo;
            purchasOrder.DispatchCity = model.DispatchCity;
            purchasOrder.DispatchVia = model.DispatchVia;
            purchasOrder.ExecutiveName = model.ExecutiveName;
            purchasOrder.Remark = model.Remark;
            purchasOrder.MaterialDelAddress = model.MaterialDelAddress;
            purchasOrder.PackedBy = model.PackedBy;
            purchasOrder.Label = model.Label;
            purchasOrder.Bottle = model.Bottle;
            purchasOrder.InvoiceNo = model.InvoiceNo;
            purchasOrder.PartyName = model.PartyName;
            purchasOrder.PartyAddress = model.PartyAddress;
            purchasOrder.SampleNumber = model.SampleNumber;
            purchasOrder.CourierChg =model.CourierChg;
            purchasOrder.PaymentTerms = model.PaymentTerms;
            purchasOrder.PO_Updated_By = currentUser.CurrentUserId;
            purchasOrder.PO_DOU = DateTime.Now;

            if (purchasOrder.PurchaseOrderDetails.Count > 1)
            {
                foreach (var item in purchasOrder.PurchaseOrderDetails)
                {
                    item.IsActive = false;
                }
            }

            model.PurchaseOrderDetails.ForEach(x =>
            {
                if (x.PurchaseOrderDetailsId != 0)
                {
                    var entity = _unitOfWork.PurchaseOrderDetails.FindAll().FirstOrDefault(y => y.PurchaseOrderDetailsId == x.PurchaseOrderDetailsId);
                    entity.ItemName = x.ItemName;
                    entity.Qty = x.Qty;
                    entity.Rate = x.Rate;
                    entity.Packing = x.Packing;
                    entity.TDPer = x.TDPer;
                    entity.BatchNo = x.BatchNo;
                    entity.InternalCode = x.InternalCode;
                    entity.MfgDate = x.MfgDate;
                    entity.IsActive = true;
                    entity.POD_DOU = DateTime.Now;
                    entity.POD_Updated_By = currentUser.CurrentUserId;
                }
                else
                {
                    purchasOrder.PurchaseOrderDetails.Add(new PurchaseOrderDetails
                    {
                        ItemName = x.ItemName,
                        Qty = x.Qty,
                        Rate = x.Rate,
                        Packing = x.Packing,
                        TDPer = x.TDPer,
                        BatchNo = x.BatchNo,
                        InternalCode = x.InternalCode,
                        MfgDate = x.MfgDate,
                        IsActive = true,
                        POD_DOE = DateTime.Now,
                        POD_Created_By = currentUser.CurrentUserId,
                    });                    
                }
            });

            _unitOfWork.Save();
            return new ResponseResults();
        }
        public int GetLastPurchaseOrderId()
        {
            var lastPurchaseOrder = _unitOfWork.PurchaseOrder.FindAll()
                .OrderByDescending(x => x.PurchaseOrderId)
                .FirstOrDefault();

            if (lastPurchaseOrder == null)
            {
                return 1;
            }
            else
            {
                return lastPurchaseOrder.PurchaseOrderId + 1;
            }
        }

        public bool CheckPONumberExists(string poNumber)
        {
            return _unitOfWork.PurchaseOrder
                .FindAll()
                .Any(x => x.PONo == poNumber && x.IsActive);
        }
    }
}
