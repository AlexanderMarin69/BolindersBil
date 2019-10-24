using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BolindersBil.web.DB;
using BolindersBil.web.Models;
using BolindersBil.web.Repositories;
using BolindersBil.web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BolindersBil.web.Controllers
{
    public class AdminController : Controller
    {
        private readonly BolindersBilDatabaseContext ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private IHostingEnvironment _appEnvironment;


        private IVehicleRepository repo;

        public int PageLimit = 2;


        public AdminController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, BolindersBilDatabaseContext context, IVehicleRepository repository, IHostingEnvironment appEnvironment)
        {
            ctx = context;
            repo = repository;
            _userManager = userManager;
            _signInManager = signInManager;
            _appEnvironment = appEnvironment;

        }



        [AllowAnonymous]
        public IActionResult Index(int page = 1)
        {

            if (User.Identity.IsAuthenticated)
            {
                var toSkip = (page - 1) * PageLimit;

                var vehicleList = ctx.Vehicles.OrderBy(x => x.Id).Skip(toSkip).Take(PageLimit);
                var paging = new PagingInfo { CurrentPage = page, ItemsPerPage = PageLimit, TotalItems = ctx.Vehicles.Count() };
                var vm = new HomeViewModel { Vehicles = vehicleList, Pager = paging };
                return View(vm);
            }
            else
            {
                return View();
            }
        }


        public async Task<IActionResult> Login(HomeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(vm.UserName);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user, vm.Password, false, false)).Succeeded)
                    {

                    }


                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }


        // Florin implemented - ok
        [HttpGet]
        public IActionResult Edit(int productId)
        {
            var vehicle = repo.Vehicles.FirstOrDefault(x => x.Id.Equals(productId));
            var vm = new EditVehicleViewModel
            {
                Brands = ctx.Brands.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),

                Bodies = ctx.Bodies.Select(x => new SelectListItem
                {
                    Text = x.BodyName,
                    Value = x.Id.ToString()
                }),

                Dealerships = ctx.Dealerships.Select(x => new SelectListItem
                {
                    Text = x.City,
                    Value = x.Id.ToString(),
                }),

                Vehicle = vehicle
            };
            return View(vm);
        }


        // Florin implemented - ok
        [HttpPost]
        public IActionResult Edit(EditVehicleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Vehicle.DateUpdated = DateTime.Now;
                repo.SaveVehicle(vm.Vehicle);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(vm);
            }
        }


        // Florin implemented - Ok
        [HttpPost]
        public IActionResult Delete(int vehicleId)
        {
            var deleted = repo.DeleteVehicle(vehicleId);
            if (deleted != null)
            {
                //product was found and deleted
            }
            else
            {
                //TODO
                //product was not found - show error
            }
            return RedirectToAction(nameof(Index));
        }


        //Florin inserted for Search function - ok
        public IActionResult Search(HomeViewModel vm, int page = 1)
        {
            var toSkip = (page - 1) * PageLimit;

            var VehicleSearchResults = ctx.Vehicles.Where(x => x.Model.Contains(vm.SearchString) || vm.SearchString == "").ToList().Skip(toSkip).Take(PageLimit);
            vm.VehiclesResults = VehicleSearchResults;
            //vm.Vehicles = ctx.Vehicles.OrderBy(x => x.Id); 

            var pagingNew = new PagingInfo { CurrentPage = page, ItemsPerPage = PageLimit, TotalItems = VehicleSearchResults.Count() };
            var vmNew = new HomeViewModel { Vehicles = vm.VehiclesResults, Pager = pagingNew };

            return View(nameof(Index), vmNew);
        }


        // Alex & Florin implemented
        public IActionResult Create()
        {
            var vm = new CreateCarViewModel
            {
                Vehicle = new Vehicle(),

                Brands = ctx.Brands.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),

                Bodies = ctx.Bodies.Select(x => new SelectListItem
                {
                    Text = x.BodyName,
                    Value = x.Id.ToString()
                }),

                Dealerships = ctx.Dealerships.Select(x => new SelectListItem
                {
                    Text = x.City,
                    Value = x.Id.ToString(),
                })
            };

            return View(vm);
        }

        // Alex & Florin implemented Ok
        //[HttpPost]
        //public async Task<IActionResult> CreateNewCar(CreateCarViewModel vm)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        vm.Vehicle.DateAdded = DateTime.Now;

        //        ctx.Vehicles.Add(vm.Vehicle);

        //        await ctx.SaveChangesAsync();
        //        return RedirectToAction("index", "Admin");
        //    }
        //    else
        //    {
        //        var vm1 = new CreateCarViewModel
        //        {
        //            Vehicle = new Vehicle(),

        //            Brands = ctx.Brands.Select(x => new SelectListItem
        //            {
        //                Text = x.Name,
        //                Value = x.Id.ToString()
        //            }),

        //            Bodies = ctx.Bodies.Select(x => new SelectListItem
        //            {
        //                Text = x.BodyName,
        //                Value = x.Id.ToString()
        //            }),

        //            Dealerships = ctx.Dealerships.Select(x => new SelectListItem
        //            {
        //                Text = x.City,
        //                Value = x.Id.ToString(),
        //            })
        //        };

        //        return View(nameof(Create), vm1);
        //    }
        //}


        [HttpPost]
        public async Task<IActionResult> CreateNewCar(CreateCarViewModel vm, ICollection<IFormFile> images)
        {

            if (ModelState.IsValid && vm != null)
            {
                var imageFolder = "\\Images";
                /* Paths to save image in disk */
                // to wwwroot
                string rootPath = _appEnvironment.WebRootPath;
                // to Images folder
                string imageFolderPath = rootPath + imageFolder;
                // to Registration folder
                string targetFolder = imageFolderPath + "\\" + vm.Vehicle.RegistrationNumber;
                /* Create Registration folder*/
                Directory.CreateDirectory(targetFolder);

                // Array to store each image
                List<FileUpload> gallery = new List<FileUpload>();

                string targetFileName = "";
                foreach (var image in images)
                {
                    Guid uniqueGuid = Guid.NewGuid();
                    targetFileName = uniqueGuid + image.FileName;
                    string finalTargetFilePath = targetFolder + "\\" + targetFileName;
                    // Replace backslash with forward slash
                    finalTargetFilePath = finalTargetFilePath.Replace("\\", "/");



                    /* Saves */

                    //Save images into RegNr folder on disk
                    if (image.Length > 0)
                    {
                        using (var stream = new FileStream(finalTargetFilePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                    }


                    // Resize and save the image under the correct folder. Calls on the ImageResize function.
                    string resizedImageFolder = targetFolder + "\\Image_resized";
                    if (!Directory.Exists(resizedImageFolder))
                    {
                        Directory.CreateDirectory(resizedImageFolder);
                    }
                    ImageResize(finalTargetFilePath, resizedImageFolder + "\\" + targetFileName, 200);



                    // Dynamik saving path
                    var imageProperty = new FileUpload
                    {
                       
                        FileTitle = uniqueGuid,
                        FilePath = imageFolder.Replace("\\", "/") + "/" + vm.Vehicle.RegistrationNumber + "/" + targetFileName

                        //Example Florin
                        //https://localhost:44356/Images/MazdaNummer3/d90cdd91-a6df-4001-89c3-6081fbf4e73dMazada3_01.jpeg
                        //https://localhost:44356/Images/MazdaNummer10/7ae213f5-08cb-472a-aa8a-5ea90f94ad83Mazada10_01.jpeg
                    };
                    gallery.Add(imageProperty);
                }

                // Save information to database
                vm.Vehicle.FileUpload = gallery;
                vm.Vehicle.DateAdded = DateTime.Now;
                
                // Florin inserted ????
                vm.Vehicle.ImageUrl = vm.Vehicle.RegistrationNumber.ToString() + "/Image_resized" + "/" + targetFileName.ToString();

                ctx.Vehicles.Add(vm.Vehicle);
                await ctx.SaveChangesAsync(); 
                return RedirectToAction("index", "Admin");
            }
            else
            {
                var vm1 = new CreateCarViewModel
                {
                    Vehicle = new Vehicle(),

                    Brands = ctx.Brands.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),

                    Bodies = ctx.Bodies.Select(x => new SelectListItem
                    {
                        Text = x.BodyName,
                        Value = x.Id.ToString()
                    }),

                    Dealerships = ctx.Dealerships.Select(x => new SelectListItem
                    {
                        Text = x.City,
                        Value = x.Id.ToString(),
                    })
                };

                return View(nameof(Create), vm1);
            }
        }

        private void ImageResize(string inputImagePath, string outputImagePath, int newWidth)
        {
            const long quality = 50L;
            Bitmap sourceBitmap = new Bitmap(inputImagePath);
            double dblWidthOriginal = sourceBitmap.Width;
            double dblHeigthOriginal = sourceBitmap.Height;
            double heightWidthRelation = dblHeigthOriginal / dblWidthOriginal;
            int newHeight = (int)(newWidth * heightWidthRelation);
            // Create empty draw area.
            var newDrawArea = new Bitmap(newWidth, newHeight);
            using (var graphic_of_DrawArea = Graphics.FromImage(newDrawArea))
            {
                graphic_of_DrawArea.CompositingQuality = CompositingQuality.HighSpeed;
                graphic_of_DrawArea.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic_of_DrawArea.CompositingMode = CompositingMode.SourceCopy;
                // Draw into placeholder.
                // Imports the image into the drawarea.
                graphic_of_DrawArea.DrawImage(sourceBitmap, 0, 0, newWidth, newHeight);
                // Output as .Jpg
                using (var output = System.IO.File.Open(outputImagePath, FileMode.Create))
                {
                    // Setup jpg
                    var qualityParamId = Encoder.Quality;
                    var encoderParameters = new EncoderParameters(1);
                    encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);
                    // Save Bitmap as Jpg
                    var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);
                    newDrawArea.Save(output, codec, encoderParameters);
                    output.Close();
                }
                graphic_of_DrawArea.Dispose();
            }
            sourceBitmap.Dispose();
        }



        // Delete Bulk - Florin implemented ????
        [HttpPost]
        public IActionResult DeleteBulk(string vehiclesIdToDelete)
        {
            if (vehiclesIdToDelete != null)
            {
                int[] vehiclesIdToDeleteArray = Array.ConvertAll(vehiclesIdToDelete.Split(','), int.Parse);

                foreach (var item in vehiclesIdToDeleteArray)
                {
                    Delete(item);
                }
            }
            return RedirectToAction(nameof(Index));
        }


        //public IActionResult DeleteBulk(IEnumerable<int> carIdToDelete)
        //{
        //    var objectToDelete = ctx.Vehicles.Where(x => carIdToDelete.Contains(x.Id)).ToList();
        //    foreach (var v in objectToDelete)
        //    {
        //        ctx.Remove(v);
        //    }
        //    ctx.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}

        //// Other method
        //[HttpPost]
        //public IActionResult DelSelEmp(string[] empids)
        //{
        //    int[] getid = null;
        //    if (empids != null)
        //    {
        //        getid = new int[empids.Length];
        //        int j = 0;
        //        foreach(string i in empids)
        //        {
        //            int.TryParse(i, out getid[j++]);
        //        }
        //        List<Vehicle> getempids = new List<Vehicle>();

        //        var vehicle = ctx.Vehicles.Where(x => x.Id.Equals(empids));

        //        foreach(var v in vehicle)
        //        {
        //            ctx.Remove(v);
        //        }
        //        ctx.SaveChanges();
        //    }
        //    return RedirectToAction(nameof(Index));
        //}
    }
}