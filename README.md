﻿# PaddleSharp 🌟 [![main](https://github.com/sdcb/PaddleSharp/actions/workflows/main.yml/badge.svg)](https://github.com/sdcb/PaddleSharp/actions/workflows/main.yml) [![QQ](https://img.shields.io/badge/QQ_Group-579060605-52B6EF?style=social&logo=tencent-qq&logoColor=000&logoWidth=20)](https://jq.qq.com/?_wv=1027&k=K4fBqpyQ)

**English** | **[简体中文](README_CN.md)**

💗 .NET Wrapper for `PaddleInference` C API, support **Windows**(x64) 💻, NVIDIA Cuda 10.2+ based GPU 🎮 and **Linux**(Ubuntu-22.04 x64) 🐧, currently contained following main components:

* [PaddleOCR 📖](./docs/ocr.md) support 14 OCR languages model download on-demand, allow rotated text angle detection, 180 degree text detection, also support table recognition 📊.
* [PaddleDetection 🎯](./docs/detection.md) support PPYolo detection model and PicoDet model 🏹.
* [RotationDetection 🔄](./docs/rotation-detection.md) use Baidu's official `text_image_orientation_infer` model to detect text picture's rotation angle(`0, 90, 180, 270`).
* [PaddleNLP ChineseSegmenter 📚](./docs/paddlenlp-lac.md) support `PaddleNLP` Lac Chinese segmenter model, supports tagging/customized words.
* [Paddle2Onnx 🔄](./docs/paddle2onnx.md) Allow user export `ONNX` model using `C#`.

## NuGet Packages/Docker Images 📦

### Release notes 📝
Please checkout [this page 📄](https://github.com/sdcb/PaddleSharp/releases).

### Infrastructure packages 🏗️

| NuGet Package 💼      | Version 📌                                                                                                            | Description 📚                         |
| -------------------- | -------------------------------------------------------------------------------------------------------------------- | ------------------------------------- |
| Sdcb.PaddleInference | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.svg)](https://nuget.org/packages/Sdcb.PaddleInference) | Paddle Inference C API .NET binding ⚙️ |

### Native Packages 🏗️

| Package                                                 | Version 📌                                                                                                                                                                      | Description                          |
| ------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | ------------------------------------ |
| Sdcb.PaddleInference.runtime.win64.mkl                  | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.win64.mkl.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.win64.mkl)                       | Recommended for most users (CPU, MKL) |
| Sdcb.PaddleInference.runtime.win64.openblas             | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.win64.openblas.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.win64.openblas)             | CPU, OpenBLAS                        |
| Sdcb.PaddleInference.runtime.win64.openblas-noavx       | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.win64.openblas-noavx.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.win64.openblas-noavx) | CPU, no AVX, for old CPUs            |
| Sdcb.PaddleInference.runtime.win64.cu118_cudnn89_sm61   | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.win64.cu118_cudnn89_sm61.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.win64.cu118_cudnn89_sm61) | CUDA 11.8, GTX 10 Series             |
| Sdcb.PaddleInference.runtime.win64.cu118_cudnn89_sm75   | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.win64.cu118_cudnn89_sm75.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.win64.cu118_cudnn89_sm75) | CUDA 11.8, RTX 20/GTX 16xx Series    |
| Sdcb.PaddleInference.runtime.win64.cu118_cudnn89_sm86   | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.win64.cu118_cudnn89_sm86.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.win64.cu118_cudnn89_sm86) | CUDA 11.8, RTX 30 Series             |
| Sdcb.PaddleInference.runtime.win64.cu118_cudnn89_sm89   | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.win64.cu118_cudnn89_sm89.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.win64.cu118_cudnn89_sm89) | CUDA 11.8, RTX 40 Series             |
| Sdcb.PaddleInference.runtime.win64.cu126_cudnn95_sm61   | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.win64.cu126_cudnn95_sm61.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.win64.cu126_cudnn95_sm61) | CUDA 12.6, GTX 10 Series             |
| Sdcb.PaddleInference.runtime.win64.cu126_cudnn95_sm75   | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.win64.cu126_cudnn95_sm75.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.win64.cu126_cudnn95_sm75) | CUDA 12.6, RTX 20/GTX 16xx Series    |
| Sdcb.PaddleInference.runtime.win64.cu126_cudnn95_sm86   | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.win64.cu126_cudnn95_sm86.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.win64.cu126_cudnn95_sm86) | CUDA 12.6, RTX 30 Series             |
| Sdcb.PaddleInference.runtime.win64.cu126_cudnn95_sm89   | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.win64.cu126_cudnn95_sm89.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.win64.cu126_cudnn95_sm89) | CUDA 12.6, RTX 40 Series             |
| Sdcb.PaddleInference.runtime.win64.cu129_cudnn910_sm61  | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.win64.cu129_cudnn910_sm61.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.win64.cu129_cudnn910_sm61) | CUDA 12.9, GTX 10 Series             |
| Sdcb.PaddleInference.runtime.win64.cu129_cudnn910_sm75  | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.win64.cu129_cudnn910_sm75.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.win64.cu129_cudnn910_sm75) | CUDA 12.9, RTX 20/GTX 16xx Series    |
| Sdcb.PaddleInference.runtime.win64.cu129_cudnn910_sm86  | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.win64.cu129_cudnn910_sm86.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.win64.cu129_cudnn910_sm86) | CUDA 12.9, RTX 30 Series             |
| Sdcb.PaddleInference.runtime.win64.cu129_cudnn910_sm89  | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.win64.cu129_cudnn910_sm89.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.win64.cu129_cudnn910_sm89) | CUDA 12.9, RTX 40 Series             |
| Sdcb.PaddleInference.runtime.win64.cu129_cudnn910_sm120 | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.win64.cu129_cudnn910_sm120.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.win64.cu129_cudnn910_sm120) | CUDA 12.9, RTX 50 Series             |
| Sdcb.PaddleInference.runtime.linux-x64.openblas         | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.linux-x64.openblas.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.linux-x64.openblas)     | Linux x64, OpenBLAS                   |
| Sdcb.PaddleInference.runtime.linux-x64.mkl              | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.linux-x64.mkl.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.linux-x64.mkl)               | Linux x64, MKL                        |
| Sdcb.PaddleInference.runtime.linux-x64                  | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.linux-x64.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.linux-x64)                       | Linux x64, MKL+OpenVINO               |
| Sdcb.PaddleInference.runtime.linux-arm64                | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.linux-arm64.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.linux-arm64)                   | Linux ARM64                           |
| Sdcb.PaddleInference.runtime.osx-x64                    | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.osx-x64.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.osx-x64)                           | macOS x64, include ONNXRuntime        |
| Sdcb.PaddleInference.runtime.osx-arm64                  | [![NuGet](https://img.shields.io/nuget/v/Sdcb.PaddleInference.runtime.osx-arm64.svg)](https://nuget.org/packages/Sdcb.PaddleInference.runtime.osx-arm64)                       | macOS ARM64                           |

**Package Selection Guide:**

- We recommend `Sdcb.PaddleInference.runtime.win64.mkl` for most users. It offers the best balance between performance and package size. Please note that this package does not support GPU acceleration, making it suitable for most general scenarios.
- `openblas-noavx` is tailored for older CPUs that do not support the AVX2 instruction set.
- The remaining packages cover various CUDA combinations (GPU acceleration), supporting three CUDA versions:
  - **CUDA 11.8:** Supports 10–40 series NVIDIA GPUs
  - **CUDA 12.6:** Supports 10–40 series NVIDIA GPUs
  - **CUDA 12.9:** Supports 10–50 series NVIDIA GPUs

**Important:**  
Not all GPU packages are suitable for every card. Please refer to the following GPU-to-`sm` suffix mapping:

| `sm` Suffix | Supported GPU Series                       |
|-------------|-------------------------------------------|
| sm61        | GTX 10 Series                            |
| sm75        | RTX 20 Series (and GTX 16xx series such as GTX 1660) |
| sm86        | RTX 30 Series                            |
| sm89        | RTX 40 Series                            |
| sm120       | RTX 50 Series (supported by CUDA 12.9 only) |

Any other packages that starts with `Sdcb.PaddleInference.runtime` might deprecated.

All packages were compiled manually by me, with some code patches from here: https://github.com/sdcb/PaddleSharp/blob/master/build/capi.patch

## Paddle Devices

* Mkldnn - `PaddleDevice.Mkldnn()`
  
  Based on [Mkldnn](https://github.com/oneapi-src/oneDNN), generally fast

* Openblas - `PaddleDevice.Openblas()`

  Based on [openblas](https://www.openblas.net/), slower, but dependencies file smaller and consume lesser memory

* Onnx - `PaddleDevice.Onnx()`

  Based on [onnxruntime](https://github.com/microsoft/onnxruntime), is also pretty fast and consume less memory

* Gpu - `PaddleDevice.Gpu()`

  Much faster but relies on NVIDIA GPU and CUDA

  If you wants to use GPU, you should refer to FAQ `How to enable GPU?` section, CUDA/cuDNN/TensorRT need to be installed manually.

## FAQ ❓
### Why my code runs good in my windows machine, but DllNotFoundException in other machine: 💻
1. Please ensure the [latest Visual C++ Redistributable](https://aka.ms/vs/17/release/vc_redist.x64.exe) was installed in `Windows` (typically it should automatically installed if you have `Visual Studio` installed) 🛠️
Otherwise, it will fail with the following error (Windows only):
   ```
   DllNotFoundException: Unable to load DLL 'paddle_inference_c' or one of its dependencies (0x8007007E)
   ```
   
   If it's Unable to load DLL OpenCvSharpExtern.dll or one of its dependencies, then most likely the Media Foundation is not installed in the Windows Server 2012 R2 machine: <img width="830" alt="image" src="https://user-images.githubusercontent.com/1317141/193706883-6a71ea83-65d9-448b-afee-2d25660430a1.png">

2. Many old CPUs do not support AVX instructions, please ensure your CPU supports AVX, or download the x64-noavx-openblas DLLs and disable Mkldnn: `PaddleDevice.Openblas()` 🚀

3. If you're using **Win7-x64**, and your CPU does support AVX2, then you might also need to extract the following 3 DLLs into `C:\Windows\System32` folder to make it run: 💾
   * api-ms-win-core-libraryloader-l1-2-0.dll
   * api-ms-win-core-processtopology-obsolete-l1-1-0.dll
   * API-MS-Win-Eventing-Provider-L1-1-0.dll
   
   You can download these 3 DLLs here: [win7-x64-onnxruntime-missing-dlls.zip](https://github.com/sdcb/PaddleSharp/files/10110622/win7-x64-onnxruntime-missing-dlls.zip) ⬇️

### How to enable GPU? 🎮
Enable GPU support can significantly improve the throughput and lower the CPU usage. 🚀

Steps to use GPU in Windows:
1. (for Windows) Install the package: `Sdcb.PaddleInference.runtime.win64.cu120*` instead of `Sdcb.PaddleInference.runtime.win64.mkl`, **do not** install both. 📦
2. Install CUDA from NVIDIA, and configure environment variables to `PATH` or `LD_LIBRARY_PATH` (Linux) 🔧
3. Install cuDNN from NVIDIA, and configure environment variables to `PATH` or `LD_LIBRARY_PATH` (Linux) 🛠️
4. Install TensorRT from NVIDIA, and configure environment variables to `PATH` or `LD_LIBRARY_PATH` (Linux) ⚙️

You can refer to this blog page for GPU in Windows: [关于PaddleSharp GPU使用 常见问题记录](https://www.cnblogs.com/cuichaohui/p/15766519.html) 📝

If you're using Linux, you need to compile your own OpenCvSharp4 environment following the [docker build scripts](./build/docker/dotnet6sdk-paddle/Dockerfile) and the CUDA/cuDNN/TensorRT configuration tasks. 🐧

After these steps are completed, you can try specifying `PaddleDevice.Gpu()` in the paddle device configuration parameter, then enjoy the performance boost! 🎉

## Thanks & Sponsors 🙏
* 崔亮  https://github.com/cuiliang
* 梁现伟
* 深圳-钱文松
* iNeuOS工业互联网操作系统：http://www.ineuos.net

## Contact 📞
QQ group of C#/.NET computer vision technical communication (C#/.NET计算机视觉技术交流群): **579060605**
![](./assets/qq.png)
