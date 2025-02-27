﻿using ReshimgathiServices.Business;
using ReshimgathiServices.Models;
using ReshimgathiServices.Responses;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReshimgathiServices.Controllers
{
    /// <summary>
    /// This controller helps to manage all types of Requested Profile Operations.
    /// </summary>
    [RoutePrefix("api/request")]
    [CatchException]
    public class RequestedProfilesController : ApiController
    {
        /// <summary>
        /// Get user Requested Profiles based on UserProfileId
        /// </summary>
        /// <returns></returns>
        [Route("checklimit/{id}")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, "Verify If Users Daily Requests Contacts Limit Exceeded Or Not.", typeof(Response<RequestedProfileResponse>))]
        public HttpResponseMessage IsTodaysLimitExceeded(Guid id)
        {
            Response<RequestedProfileResponse> upr = new Response<RequestedProfileResponse>();
            try
            {
                UserProfileOperations uop = new UserProfileOperations();
                RequestedProfileOperations rpo = new RequestedProfileOperations();
                var userDetails = uop.GetUserProfileDetails(id);

                if (userDetails != null)
                {
                    upr.Message = "User Profile found.";
                    upr.HttpStatus = HttpStatusCode.OK.ToString();
                    upr.ResponseObj = new RequestedProfileResponse()
                    {
                        LimitExceeded = rpo.CheckLimitExceededForToday(userDetails.Id),
                    };
                }
                else
                {
                    upr.Message = "User Profile Not Found.";
                    upr.ResponseObj = new RequestedProfileResponse()
                    {
                        LimitExceeded = false
                    };
                }

                upr.AdditionalMessage = "Additional note found here.";
                upr.HttpStatus = HttpStatusCode.OK.ToString();
                upr.Success = true;
            }
            catch (Exception e)
            {
                upr.Success = false;
                upr.Message = "Internal Server error. Please contact admin or try after some time.";
                upr.AdditionalMessage = e.Message;
                upr.HttpStatus = HttpStatusCode.InternalServerError.ToString();
                upr.ResponseObj = new RequestedProfileResponse()
                {
                    LimitExceeded = false
                };
            }

            return Request.CreateResponse(HttpStatusCode.OK, upr);
        }


        /// <summary>
        /// Get user Requested Profiles based on UserProfileId
        /// </summary>
        /// <returns></returns>
        [Route("all/{id}")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, "Get All Request Contacts Date Wise Of Given User Profile Id.", typeof(Response<AllRequestedProfileResponse>))]
        public HttpResponseMessage GetAllRequests(Guid id)
        {
            Response<AllRequestedProfileResponse> upr = new Response<AllRequestedProfileResponse>();
            try
            {
                UserProfileOperations uop = new UserProfileOperations();
                RequestedProfileOperations rpo = new RequestedProfileOperations();
                var userDetails = uop.GetUserProfileDetails(id);

                if (userDetails != null)
                {
                    upr.Message = "User Profile found.";
                    upr.HttpStatus = HttpStatusCode.OK.ToString();
                    upr.ResponseObj = new AllRequestedProfileResponse()
                    {
                        RequestedProfiles = rpo.GetAllRequestedProfiles(userDetails.Id),
                    };
                }
                else
                {
                    upr.Message = "User Profile Not Found.";
                    upr.ResponseObj = new AllRequestedProfileResponse()
                    {
                        RequestedProfiles = null
                    };
                }

                upr.AdditionalMessage = "Additional note found here.";
                upr.HttpStatus = HttpStatusCode.OK.ToString();
                upr.Success = true;
            }
            catch (Exception e)
            {
                upr.Success = false;
                upr.Message = "Internal Server error. Please contact admin or try after some time.";
                upr.AdditionalMessage = e.Message;
                upr.HttpStatus = HttpStatusCode.InternalServerError.ToString();
                upr.ResponseObj = new AllRequestedProfileResponse()
                {
                    RequestedProfiles = null
                };
            }

            return Request.CreateResponse(HttpStatusCode.OK, upr);
        }

        [Route("add")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, "Save Requested Profile Data For a Given User Profile Id.", typeof(Response<SaveRequestedProfileResponse>))]
        public HttpResponseMessage SaveRequestedProfiles(RequestedProfiles req)
        {
            Response<SaveRequestedProfileResponse> upr = new Response<SaveRequestedProfileResponse>();
            bool requestedProfileStatus = false;

            try
            {
                UserProfileOperations uop = new UserProfileOperations();
                var userDetails = uop.GetUserProfileDetails(req.Id);

                if (userDetails != null)
                {
                    RequestedProfileOperations fav = new RequestedProfileOperations();
                    requestedProfileStatus = fav.Save(req);

                    upr.Message = "User Profile found.";
                    upr.HttpStatus = HttpStatusCode.OK.ToString();
                    upr.ResponseObj = new SaveRequestedProfileResponse()
                    {
                        RequestedProfilesSaved = requestedProfileStatus
                    };
                }
                else
                {
                    upr.Message = "User Profile Not Found.";
                    upr.ResponseObj = new SaveRequestedProfileResponse()
                    {
                        RequestedProfilesSaved = requestedProfileStatus
                    };
                }
            }
            catch (Exception e)
            {
                upr.Success = false;
                upr.Message = "Internal Server error. Please contact admin or try after some time.";
                upr.AdditionalMessage = e.Message;
                upr.HttpStatus = HttpStatusCode.InternalServerError.ToString();
                upr.ResponseObj = new SaveRequestedProfileResponse()
                {
                    RequestedProfilesSaved = requestedProfileStatus
                };
            }

            return Request.CreateResponse(HttpStatusCode.Created, upr);
        }

        [Route("remove")]
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.Created, "Remove Requested Profile Data For a Given User Profile Id.", typeof(Response<RemoveRequestedProfileResponse>))]
        public HttpResponseMessage RemoveFavouriteProfiles(RequestedProfiles req)
        {
            Response<RemoveRequestedProfileResponse> upr = new Response<RemoveRequestedProfileResponse>();
            bool requestedProfileStatus = false;

            try
            {
                UserProfileOperations uop = new UserProfileOperations();
                var userDetails = uop.GetUserProfileDetails(req.Id);

                if (userDetails != null)
                {
                    RequestedProfileOperations fav = new RequestedProfileOperations();
                    requestedProfileStatus = fav.Delete(req);

                    upr.Message = "User Profile found.";
                    upr.HttpStatus = HttpStatusCode.OK.ToString();
                    upr.ResponseObj = new RemoveRequestedProfileResponse()
                    {
                        RequestedProfilesRemoved = requestedProfileStatus
                    };
                }
                else
                {
                    upr.Message = "User Profile Not Found.";
                    upr.ResponseObj = new RemoveRequestedProfileResponse()
                    {
                        RequestedProfilesRemoved = requestedProfileStatus
                    };
                }
            }
            catch (Exception e)
            {
                upr.Success = false;
                upr.Message = "Internal Server error. Please contact admin or try after some time.";
                upr.AdditionalMessage = e.Message;
                upr.HttpStatus = HttpStatusCode.InternalServerError.ToString();
                upr.ResponseObj = new RemoveRequestedProfileResponse()
                {
                    RequestedProfilesRemoved = requestedProfileStatus
                };
            }

            return Request.CreateResponse(HttpStatusCode.Created, upr);
        }
    }
}
