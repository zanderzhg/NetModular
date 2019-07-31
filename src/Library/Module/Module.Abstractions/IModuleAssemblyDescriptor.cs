﻿using System.Reflection;

namespace Nm.Lib.Module.Abstractions
{
    /// <summary>
    /// 模块的程序集描述
    /// </summary>
    public interface IModuleAssemblyDescriptor
    {
        /// <summary>
        /// 应用层服务
        /// </summary>
        Assembly Application { get; set; }

        /// <summary>
        /// 领域
        /// </summary>
        Assembly Domain { get; set; }

        /// <summary>
        /// 基础设施
        /// </summary>
        Assembly Infrastructure { get; set; }
    }
}