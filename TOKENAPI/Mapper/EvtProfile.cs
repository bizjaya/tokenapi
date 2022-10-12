using AutoMapper;
using TOKENAPI.Domain;
using TOKENAPI.Events;
using TOKENAPI.Models;
using System.Numerics;
using TOKENAPI.Common;

namespace TOKENAPI.Mapper
{
    public class EvtProfile : Profile
    {

        private readonly Stgs _stgs;

        public EvtProfile(Stgs stgs)
        {
            _stgs = stgs;
            var curDt = DateTime.Now;
            var curMk = curDt.ToUnix();


            CreateMap<TransferEvt, EvtTransfer>()
                .ForMember(x=>x.from, s => s.MapFrom(c=> c.from))
                .ForMember(x => x.to, s => s.MapFrom(c => c.to))
                .ForMember(x => x.value_, s => s.MapFrom(c => c.value.FrWei(_stgs.Decimals)))
                .ForMember(x => x.crtd, s => s.MapFrom(c => curMk))
                .ForMember(x => x.crtd_, s => s.MapFrom(c => curDt))
                .ForMember(x => x.crtd_, s => s.MapFrom(c => curDt))
                .ForMember(x => x.txid, s => s.MapFrom(c => c.txid))

                ;
            CreateMap<StakedEvt, EvtStaked>()
                //.ForMember(x => x.timestamp, s=>s.Ignore())
                .ForMember(x => x.timestamp_, s => s.Ignore())
                .ForMember(x => x.user, s => s.MapFrom(c => c.user))
                .ForMember(x => x.amount_, s => s.MapFrom(c => c.amount.FrWei(_stgs.Decimals)))
                .ForMember(x => x.total_, s => s.MapFrom(c => c.total.FrWei(_stgs.Decimals)))
                .ForMember(x => x.unamt_, s => s.MapFrom(c => c.unamt.FrWei(_stgs.Decimals)))
                .ForMember(x => x.crtd, s => s.MapFrom(c => curMk))
                .ForMember(x => x.crtd_, s => s.MapFrom(c => curDt))
                .ForMember(x => x.txid, s => s.MapFrom(c => c.txid))
                ;
            CreateMap<UnstakedEvt, EvtUnstaked>()
                //.ForMember(x => x.timestamp, s => s.Ignore())
                .ForMember(x => x.timestamp_, s => s.Ignore())
                .ForMember(x => x.user, s => s.MapFrom(c => c.user))
                .ForMember(x => x.amount_, s => s.MapFrom(c => c.amount.FrWei(_stgs.Decimals)))
                .ForMember(x => x.total_, s => s.MapFrom(c => c.total.FrWei(_stgs.Decimals)))
                .ForMember(x => x.unamt_, s => s.MapFrom(c => c.unamt.FrWei(_stgs.Decimals)))
                .ForMember(x => x.crtd, s => s.MapFrom(c => curMk))
                .ForMember(x => x.crtd_, s => s.MapFrom(c => curDt))
                .ForMember(x => x.txid, s => s.MapFrom(c => c.txid))

                ;

            CreateMap<ClaimedEvt, EvtClaimed>()
                //.ForMember(x => x.timestamp, s => s.Ignore())
                .ForMember(x => x.timestamp_, s => s.Ignore())
                .ForMember(x => x.user, s => s.MapFrom(c => c.user))      
                .ForMember(x => x.amount_, s => s.MapFrom(c => c.amount.FrWei(_stgs.Decimals)))
                .ForMember(x => x.total_, s => s.MapFrom(c => c.total.FrWei(_stgs.Decimals)))
                .ForMember(x => x.unamt_, s => s.MapFrom(c => c.unamt.FrWei(_stgs.Decimals)))
                .ForMember(x => x.crtd, s => s.MapFrom(c => curMk))
                .ForMember(x => x.crtd_, s => s.MapFrom(c => curDt))
                .ForMember(x => x.txid, s => s.MapFrom(c => c.txid))
                ;

            CreateMap<XtkEvt, EvtXtk>()
                .ForMember(x => x.ts_, s => s.MapFrom(c=>c.ts.ToDate()))
                .ForMember(x => x.addr, s => s.MapFrom(c => c.addr))
                .ForMember(x => x.bno, s => s.MapFrom(c => c.bno))
                .ForMember(x => x.amt_, s => s.MapFrom(c => c.amt.FrWei(_stgs.Decimals)))

                ;


            //CreateMap<EvtTransfer, TransferEvt>();
            //CreateMap<EvtApproval, ApprovalEvt>();
            //CreateMap<EvtClaimed, ClaimedEvt>();

            //CreateMap<EvtPurchased, PurchasedEvt>();
            //CreateMap<EvtStaked, StakedEvt>();
            //CreateMap<EvtUnstaked, UnstakedEvt>();


        }
    }
}
