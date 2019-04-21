﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MotorregisterAPI.Generation.Models
{
    public class MotorregisterDataItem
    {
        public int VehicleID { get; set; }
        public string VehicleIdentityNumber { get; set; }
        public string VehicleKindNumber { get; set; }
        public string VehicleKindName { get; set; }
        public string VehicleUsageNumber { get; set; }
        public string VehicleUsageName { get; set; }
        public string VehiclePostalCode { get; set; }
        public DateTime? LeasingValidFrom { get; set; }
        public DateTime? LeasingValidTo { get; set; }
        public string LicensePlateNumber { get; set; }
        public DateTime? LicensePlateValidFrom { get; set; }
        public DateTime? LicensePlateValidTo { get; set; }
        public DateTime? LicensePlateExpirationDate { get; set; }
        public string VehicleInfoCreatedFrom { get; set; }
        public string VehicleInfoStatus { get; set; }
        public DateTime? VehicleInfoStatusDate { get; set; }
        public DateTime? VehicleInfoFirstRegistrationDate { get; set; }
        public string VehicleInfoVINNumber { get; set; }
        public string VehicleInfoVINNumberPlacement { get; set; }
        public string VehicleInfoModelYear { get; set; }
        public string VehicleInfoTotalWeight { get; set; }
        public string VehicleInfoOwnWeight { get; set; }
        public string VehicleInfoRunningWeightMinimum { get; set; }
        public string VehicleInfoRunningWeightMaximum { get; set; }
        public string VehicleInfoTechnicalTotalWeight { get; set; }
        public string VehicleInfoRoadTrainWeight { get; set; }
        public string VehicleInfoAxleCount { get; set; }
        public string VehicleInfoAxlePressure { get; set; }
        public string VehicleInfoPassengerCount { get; set; }
        public string VehicleInfoSeatingSpotsMinimum { get; set; }
        public string VehicleInfoSeatingSpotsMaximum { get; set; }
        public string VehicleInfoStandingSpotsMinimum { get; set; }
        public string VehicleInfoStandingSpotsMaximum { get; set; }
        public string VehicleInfoConnectionOption { get; set; }
        public string VehicleInfoConnectionWeightWithoutBreaks { get; set; }
        public string VehicleInfoConnectionWeightWithBreaks { get; set; }
        public string VehicleInfoTrailerTotalWeight { get; set; }
        public string VehicleInfoStoolLoad { get; set; }
        public string VehicleInfoSemiTrailerAllowedAxlePressure { get; set; }
        public string VehicleInfoMaximumSpeed { get; set; }
        public string VehicleInfoRimTires { get; set; }
        public string VehicleInfoAttachedSidecarVINNumber { get; set; }
        public string VehicleInfoNCAPTest { get; set; }
        public string VehicleInfoVValueAir { get; set; }
        public string VehicleInfoVValueMechanical { get; set; }
        public string VehicleInfoOtherEquipment { get; set; }
        public string VehicleInfoVehicleCondition { get; set; }
        public string VehicleInfo30PercentVan { get; set; }
        public string VehicleInfoPullingAxles { get; set; }
        public string VehicleInfoSuitableForTaxi { get; set; }
        public string VehicleInfoAxleDistance { get; set; }
        public string VehicleInfoGaugeFront { get; set; }
        public string VehicleInfoGaugeBack { get; set; }
        public string VehicleInfoTypeNotificationNumber { get; set; }
        public string VehicleInfoTypeApprovalNumber { get; set; }
        public string VehicleInfoEUVariant { get; set; }
        public string VehicleInfoEUVersion { get; set; }
        public string VehicleInfoComment { get; set; }
        public string VehicleInfoTypeApprovedCategory { get; set; }
        public string VehicleInfoGearCount { get; set; }
        public string VehicleInfoDoorCount { get; set; }
        public string VehicleInfoManufacturer { get; set; }
        public string VehicleInfoRoadAirSuspension { get; set; }
        public string VehicleInfoDanishApprovalNumber { get; set; }
        public DateTime? VehicleInfoYear { get; set; }
        public DateTime? VehicleInfoStartupDate { get; set; }
        public string VehicleInfoTrafficDamage { get; set; }
        public string VehicleInfoVeteranVehicleOriginal { get; set; }
        public string VehicleInfoBrandTypeNumber { get; set; }
        public string VehicleInfoBrandTypeName { get; set; }
        public string VehicleInfoModelTypeNumber { get; set; }
        public string VehicleInfoModelTypeName { get; set; }
        public string VehicleInfoVariantTypeNumber { get; set; }
        public string VehicleInfoVariantTypeName { get; set; }
        public string VehicleInfoKindTypeNumber { get; set; }
        public string VehicleInfoKindTypeName { get; set; }
        public string VehicleInfoColorTypeNumber { get; set; }
        public string VehicleInfoColorTypeName { get; set; }
        public string VehicleInfoBodyTypeNumber { get; set; }
        public string VehicleInfoBodyTypeName { get; set; }
        public string VehicleInfoNormTypeNumber { get; set; }
        public string VehicleInfoNormTypeName { get; set; }
        public string VehicleEnvironmentCO2Release { get; set; }
        public string VehicleEnvironmentEmissionCO { get; set; }
        public string VehicleEnvironmentEmissionHCPlusNOX { get; set; }
        public string VehicleEnvironmentEmissionNOX { get; set; }
        public string VehicleEnvironmentParticles { get; set; }
        public string VehicleEnvironmentOpacity { get; set; }
        public string VehicleEnvironmentParticleFilter { get; set; }
        public string VehicleEnvironmentSmokeDensity { get; set; }
        public string VehicleEnvironmentRetrofitParticleFilter { get; set; }
        public string VehicleMotorCylinderCount { get; set; }
        public string VehicleMotorEngineCapacity { get; set; }
        public string VehicleMotorBiggestEffect { get; set; }
        public string VehicleMotorKmPerLiter { get; set; }
        public string VehicleMotorKmPerLiterPreCalc { get; set; }
        public string VehicleMotorPlugInHybrid { get; set; }
        public string VehicleMotorMileage { get; set; }
        public string VehicleMotorMileageDocumentation { get; set; }
        public string VehicleMotorMarking { get; set; }
        public string VehicleMotorIdleSoundLevel { get; set; }
        public string VehicleMotorRunningSoundLevel { get; set; }
        public string VehicleMotorIdleSoundLevelSpeed { get; set; }
        public string VehicleMotorInnovativeTech { get; set; }
        public string VehicleMotorInnovativeTechCount { get; set; }
        public string VehicleMotorElectricalConsumption { get; set; }
        public string VehicleMotorFuelMode { get; set; }
        public string VehicleMotorGasConsumption { get; set; }
        public string VehicleMotorElectricalRange { get; set; }
        public string VehicleMotorBatteryCapacity { get; set; }
        public string VehicleMotorDrivingForceNumber { get; set; }
        public string VehicleMotorDrivingForceName { get; set; }
        public string VehicleInspectionResultType { get; set; }
        public DateTime? VehicleInspectionResultDate { get; set; }
        public string VehicleInspectionResult { get; set; }
        public string VehicleInspectionResultStatus { get; set; }
        public DateTime? VehicleInspectionResultStatusDate { get; set; }
        public string VehicleInspectionResultMileage { get; set; }
        public DateTime? VehicleInspectionResultReinspectionMeetingDate { get; set; }
        public string VehicleRegistrationStatus { get; set; }
        public DateTime? VehicleRegistrationDate { get; set; }
    }
}
