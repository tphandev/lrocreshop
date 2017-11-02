﻿using LrocreShop.Common;
using LrocreShop.Data.Infrastructure;
using LrocreShop.Data.Repositories;
using LrocreShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LrocreShop.Service
{
    public interface ICommonService
    {
        Footer GetFooter();
    }
    public class CommonService : ICommonService
    {
        private IFooterRepository _footerRepository;
        private IUnitOfWork _unitOfWork;
        public CommonService(IFooterRepository footerRepository, IUnitOfWork unitOfWork)
        {
            _footerRepository = footerRepository;
            _unitOfWork = unitOfWork;
        }
        public Footer GetFooter()
        {
            return _footerRepository.GetSingleByCondition(x => x.ID == CommonConstants.DefaultFooterId);
        }
    }
}
