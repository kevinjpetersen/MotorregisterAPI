using MongoDB.Driver;
using MotorregisterAPI.Generation.Helpers;
using MotorregisterAPI.Generation.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;

namespace MotorregisterAPI.Generation
{
    public class Generation
    {
        private string InputFileName;
        private int SavingRowsInternal;

        public void Generate()
        {
            Thread thr = new Thread(() =>
            {
                var client = new MongoClient("MONGO_CONNECTIONSTRING");
                var database = client.GetDatabase("MONGO_DATABASE");
                var collection = database.GetCollection<MotorregisterDataItem>("MONGO_MOTORREGISTER_DATA_COLLECTION");

                var mrItems = new List<MotorregisterDataItem>();
                var xmlFile = Path.Combine(Environment.CurrentDirectory, InputFileName);

                XmlReader reader = XmlReader.Create(xmlFile);

                reader.ReadToDescendant("ns:ESStatistikListeModtag_I");
                reader.ReadToDescendant("ns:StatistikSamling");
                reader.ReadToDescendant("ns:Statistik");

                var currentProccessedItem = 1;

                do
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(reader.ReadOuterXml());
                    XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
                    ns.AddNamespace("ns", "http://skat.dk/dmr/2007/05/31/");

                    string t = doc.OuterXml.Replace(@"ns=\", "ns=");
                    XmlNode item = doc.DocumentElement;

                    int index = t.IndexOf(@"ns=\");
                    string x = item.InnerXml;

                    ESStatistikListeModtag_ITypeStatistik statistic = XMLHelper.Deserialize<ESStatistikListeModtag_ITypeStatistik>(t);
                    KoeretoejOplysningGrundStrukturType vehicleInfo = (statistic.KoeretoejOplysningGrundStruktur != null ? statistic.KoeretoejOplysningGrundStruktur : null);

                    DateTime dfTime = DateTime.Parse("0001-01-01 00:00:00");

                    var dataItem = new MotorregisterDataItem();
                    dataItem.VehicleIdentityNumber = statistic.KoeretoejIdent;
                    dataItem.VehicleKindNumber = statistic.KoeretoejArtNummer;
                    dataItem.VehicleKindName = statistic.KoeretoejArtNavn;
                    dataItem.VehicleUsageNumber = (statistic.KoeretoejAnvendelseStruktur != null ? statistic.KoeretoejAnvendelseStruktur.KoeretoejAnvendelseNummer : null);
                    dataItem.VehicleUsageName = (statistic.KoeretoejAnvendelseStruktur != null ? statistic.KoeretoejAnvendelseStruktur.KoeretoejAnvendelseNavn : null);
                    dataItem.VehiclePostalCode = (statistic.AdressePostNummer != null ? statistic.AdressePostNummer : null);
                    dataItem.LeasingValidFrom = (statistic.LeasingGyldigFra != null ? statistic.LeasingGyldigFra : dfTime);
                    dataItem.LeasingValidTo = (statistic.LeasingGyldigTil != null ? statistic.LeasingGyldigTil : dfTime);
                    dataItem.LicensePlateNumber = statistic.RegistreringNummerNummer;
                    dataItem.LicensePlateValidFrom = (statistic.RegistreringNummerRettighedGyldigFra != null ? statistic.RegistreringNummerRettighedGyldigFra : dfTime);
                    dataItem.LicensePlateValidTo = (statistic.RegistreringNummerRettighedGyldigTil != null ? statistic.RegistreringNummerRettighedGyldigTil : dfTime);
                    dataItem.LicensePlateExpirationDate = (statistic.RegistreringNummerUdloebDato != null ? statistic.RegistreringNummerUdloebDato : dfTime);
                    dataItem.VehicleInfoCreatedFrom = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningOprettetUdFra.ToString() : null);
                    dataItem.VehicleInfoStatus = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningStatus.ToString() : null);
                    dataItem.VehicleInfoStatusDate = (vehicleInfo != null ? (vehicleInfo.KoeretoejOplysningStatusDato != null ? vehicleInfo.KoeretoejOplysningStatusDato : dfTime) : dfTime);
                    dataItem.VehicleInfoFirstRegistrationDate = (vehicleInfo != null ? (vehicleInfo.KoeretoejOplysningFoersteRegistreringDato != null ? vehicleInfo.KoeretoejOplysningFoersteRegistreringDato : dfTime) : dfTime);
                    dataItem.VehicleInfoVINNumber = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningStelNummer : null);
                    dataItem.VehicleInfoVINNumberPlacement = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningStelNummerAnbringelse : null);
                    dataItem.VehicleInfoModelYear = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningModelAar : null);
                    dataItem.VehicleInfoTotalWeight = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningTotalVaegt : null);
                    dataItem.VehicleInfoOwnWeight = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningEgenVaegt : null);
                    dataItem.VehicleInfoRunningWeightMinimum = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningKoereklarVaegtMinimum : null);
                    dataItem.VehicleInfoRunningWeightMaximum = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningKoereklarVaegtMaksimum : null);
                    dataItem.VehicleInfoTechnicalTotalWeight = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningTekniskTotalVaegt : null);
                    dataItem.VehicleInfoRoadTrainWeight = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningVogntogVaegt : null);
                    dataItem.VehicleInfoAxleCount = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningAkselAntal : null);
                    dataItem.VehicleInfoAxlePressure = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningStoersteAkselTryk : null);
                    dataItem.VehicleInfoPassengerCount = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningPassagerAntal : null);
                    dataItem.VehicleInfoSeatingSpotsMinimum = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningSiddepladserMinimum : null);
                    dataItem.VehicleInfoSeatingSpotsMaximum = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningSiddepladserMaksimum : null);
                    dataItem.VehicleInfoStandingSpotsMinimum = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningStaapladserMinimum : null);
                    dataItem.VehicleInfoStandingSpotsMaximum = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningStaapladserMaksimum : null);
                    dataItem.VehicleInfoConnectionOption = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningTilkoblingMulighed.ToString() : null);
                    dataItem.VehicleInfoConnectionWeightWithoutBreaks = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningTilkoblingsvaegtUdenBremser : null);
                    dataItem.VehicleInfoConnectionWeightWithBreaks = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningTilkoblingsvaegtMedBremser : null);
                    dataItem.VehicleInfoTrailerTotalWeight = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningPaahaengVognTotalVaegt : null);
                    dataItem.VehicleInfoStoolLoad = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningSkammelBelastning : null);
                    dataItem.VehicleInfoSemiTrailerAllowedAxlePressure = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningSaettevognTilladtAkselTryk : null);
                    dataItem.VehicleInfoMaximumSpeed = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningMaksimumHastighed : null);
                    dataItem.VehicleInfoRimTires = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningFaelgDaek : null);
                    dataItem.VehicleInfoAttachedSidecarVINNumber = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningTilkobletSidevognStelnr : null);
                    dataItem.VehicleInfoNCAPTest = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningNCAPTest.ToString() : null);
                    dataItem.VehicleInfoVValueAir = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningVVaerdiLuft.ToString() : null);
                    dataItem.VehicleInfoVValueMechanical = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningVVaerdiMekanisk.ToString() : null);
                    dataItem.VehicleInfoOtherEquipment = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningOevrigtUdstyr : null);
                    dataItem.VehicleInfoVehicleCondition = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningKoeretoejstand.ToString() : null);
                    dataItem.VehicleInfo30PercentVan = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysning30PctVarevogn.ToString() : null);
                    dataItem.VehicleInfoPullingAxles = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningTraekkendeAksler : null);
                    dataItem.VehicleInfoSuitableForTaxi = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningEgnetTilTaxi.ToString() : null);
                    dataItem.VehicleInfoAxleDistance = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningAkselAfstand : null);
                    dataItem.VehicleInfoGaugeFront = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningSporviddenForrest : null);
                    dataItem.VehicleInfoGaugeBack = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningSporviddenBagest : null);
                    dataItem.VehicleInfoTypeNotificationNumber = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningTypeAnmeldelseNummer : null);
                    dataItem.VehicleInfoTypeApprovalNumber = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningTypeGodkendelseNummer : null);
                    dataItem.VehicleInfoEUVariant = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningEUVariant : null);
                    dataItem.VehicleInfoEUVersion = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningEUVersion : null);
                    dataItem.VehicleInfoComment = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningKommentar : null);
                    dataItem.VehicleInfoTypeApprovedCategory = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningTypegodkendtKategori : null);
                    dataItem.VehicleInfoGearCount = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningAntalGear : null);
                    dataItem.VehicleInfoDoorCount = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningAntalDoere : null);
                    dataItem.VehicleInfoManufacturer = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningFabrikant : null);
                    dataItem.VehicleInfoRoadAirSuspension = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningVejvenligLuftaffjedring.ToString() : null);
                    dataItem.VehicleInfoDanishApprovalNumber = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningDanskGodkendelseNummer : null);
                    dataItem.VehicleInfoYear = (vehicleInfo != null ? (vehicleInfo.KoeretoejOplysningAargang != null ? vehicleInfo.KoeretoejOplysningAargang : dfTime) : dfTime);
                    dataItem.VehicleInfoStartupDate = (vehicleInfo != null ? (vehicleInfo.KoeretoejOplysningIbrugtagningDato != null ? vehicleInfo.KoeretoejOplysningIbrugtagningDato : dfTime) : dfTime);
                    dataItem.VehicleInfoTrafficDamage = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningTrafikskade.ToString() : null);
                    dataItem.VehicleInfoVeteranVehicleOriginal = (vehicleInfo != null ? vehicleInfo.KoeretoejOplysningVeteranKoeretoejOriginal.ToString() : null);
                    dataItem.VehicleInfoBrandTypeNumber = (vehicleInfo != null ? (vehicleInfo.KoeretoejBetegnelseStruktur != null ? vehicleInfo.KoeretoejBetegnelseStruktur.KoeretoejMaerkeTypeNummer : null) : null);
                    dataItem.VehicleInfoBrandTypeName = (vehicleInfo != null ? (vehicleInfo.KoeretoejBetegnelseStruktur != null ? vehicleInfo.KoeretoejBetegnelseStruktur.KoeretoejMaerkeTypeNavn : null) : null);
                    dataItem.VehicleInfoModelTypeNumber = (vehicleInfo != null ? (vehicleInfo.KoeretoejBetegnelseStruktur != null ? (vehicleInfo.KoeretoejBetegnelseStruktur.Model != null ? vehicleInfo.KoeretoejBetegnelseStruktur.Model.KoeretoejModelTypeNummer : null) : null) : null);
                    dataItem.VehicleInfoModelTypeName = (vehicleInfo != null ? (vehicleInfo.KoeretoejBetegnelseStruktur != null ? (vehicleInfo.KoeretoejBetegnelseStruktur.Model != null ? vehicleInfo.KoeretoejBetegnelseStruktur.Model.KoeretoejModelTypeNavn : null) : null) : null);
                    dataItem.VehicleInfoVariantTypeNumber = (vehicleInfo != null ? (vehicleInfo.KoeretoejBetegnelseStruktur != null ? (vehicleInfo.KoeretoejBetegnelseStruktur.Variant != null ? vehicleInfo.KoeretoejBetegnelseStruktur.Variant.KoeretoejVariantTypeNummer : null) : null) : null);
                    dataItem.VehicleInfoVariantTypeName = (vehicleInfo != null ? (vehicleInfo.KoeretoejBetegnelseStruktur != null ? (vehicleInfo.KoeretoejBetegnelseStruktur.Variant != null ? vehicleInfo.KoeretoejBetegnelseStruktur.Variant.KoeretoejVariantTypeNavn : null) : null) : null);
                    dataItem.VehicleInfoKindTypeNumber = (vehicleInfo != null ? (vehicleInfo.KoeretoejBetegnelseStruktur != null ? (vehicleInfo.KoeretoejBetegnelseStruktur.Type != null ? vehicleInfo.KoeretoejBetegnelseStruktur.Type.KoeretoejTypeTypeNummer : null) : null) : null);
                    dataItem.VehicleInfoKindTypeName = (vehicleInfo != null ? (vehicleInfo.KoeretoejBetegnelseStruktur != null ? (vehicleInfo.KoeretoejBetegnelseStruktur.Type != null ? vehicleInfo.KoeretoejBetegnelseStruktur.Type.KoeretoejTypeTypeNavn : null) : null) : null);
                    dataItem.VehicleInfoColorTypeNumber = (vehicleInfo != null ? (vehicleInfo.KoeretoejFarveStruktur != null ? (vehicleInfo.KoeretoejFarveStruktur.FarveTypeStruktur != null ? vehicleInfo.KoeretoejFarveStruktur.FarveTypeStruktur.FarveTypeNummer : null) : null) : null);
                    dataItem.VehicleInfoColorTypeName = (vehicleInfo != null ? (vehicleInfo.KoeretoejFarveStruktur != null ? (vehicleInfo.KoeretoejFarveStruktur.FarveTypeStruktur != null ? vehicleInfo.KoeretoejFarveStruktur.FarveTypeStruktur.FarveTypeNavn : null) : null) : null);
                    dataItem.VehicleInfoBodyTypeNumber = (vehicleInfo != null ? (vehicleInfo.KarrosseriTypeStruktur != null ? vehicleInfo.KarrosseriTypeStruktur.KarrosseriTypeNummer : null) : null);
                    dataItem.VehicleInfoBodyTypeName = (vehicleInfo != null ? (vehicleInfo.KarrosseriTypeStruktur != null ? vehicleInfo.KarrosseriTypeStruktur.KarrosseriTypeNavn : null) : null);
                    dataItem.VehicleInfoNormTypeNumber = (vehicleInfo != null ? (vehicleInfo.KoeretoejNormStruktur != null ? (vehicleInfo.KoeretoejNormStruktur.NormTypeStruktur != null ? vehicleInfo.KoeretoejNormStruktur.NormTypeStruktur.NormTypeNummer : null) : null) : null);
                    dataItem.VehicleInfoNormTypeName = (vehicleInfo != null ? (vehicleInfo.KoeretoejNormStruktur != null ? (vehicleInfo.KoeretoejNormStruktur.NormTypeStruktur != null ? vehicleInfo.KoeretoejNormStruktur.NormTypeStruktur.NormTypeNavn : null) : null) : null);
                    dataItem.VehicleEnvironmentCO2Release = (vehicleInfo != null ? (vehicleInfo.KoeretoejMiljoeOplysningStruktur != null ? vehicleInfo.KoeretoejMiljoeOplysningStruktur.KoeretoejMiljoeOplysningCO2Udslip.ToString() : null) : null);
                    dataItem.VehicleEnvironmentEmissionCO = (vehicleInfo != null ? (vehicleInfo.KoeretoejMiljoeOplysningStruktur != null ? vehicleInfo.KoeretoejMiljoeOplysningStruktur.KoeretoejMiljoeOplysningEmissionCO.ToString() : null) : null);
                    dataItem.VehicleEnvironmentEmissionHCPlusNOX = (vehicleInfo != null ? (vehicleInfo.KoeretoejMiljoeOplysningStruktur != null ? vehicleInfo.KoeretoejMiljoeOplysningStruktur.KoeretoejMiljoeOplysningEmissionHCPlusNOX.ToString() : null) : null);
                    dataItem.VehicleEnvironmentEmissionNOX = (vehicleInfo != null ? (vehicleInfo.KoeretoejMiljoeOplysningStruktur != null ? vehicleInfo.KoeretoejMiljoeOplysningStruktur.KoeretoejMiljoeOplysningEmissionNOX.ToString() : null) : null);
                    dataItem.VehicleEnvironmentParticles = (vehicleInfo != null ? (vehicleInfo.KoeretoejMiljoeOplysningStruktur != null ? vehicleInfo.KoeretoejMiljoeOplysningStruktur.KoeretoejMiljoeOplysningPartikler.ToString() : null) : null);
                    dataItem.VehicleEnvironmentOpacity = (vehicleInfo != null ? (vehicleInfo.KoeretoejMiljoeOplysningStruktur != null ? vehicleInfo.KoeretoejMiljoeOplysningStruktur.KoeretoejMiljoeOplysningRoegtaethed.ToString() : null) : null);
                    dataItem.VehicleEnvironmentParticleFilter = (vehicleInfo != null ? (vehicleInfo.KoeretoejMiljoeOplysningStruktur != null ? vehicleInfo.KoeretoejMiljoeOplysningStruktur.KoeretoejMiljoeOplysningPartikelFilter.ToString() : null) : null);
                    dataItem.VehicleEnvironmentSmokeDensity = (vehicleInfo != null ? (vehicleInfo.KoeretoejMiljoeOplysningStruktur != null ? vehicleInfo.KoeretoejMiljoeOplysningStruktur.KoeretoejMiljoeOplysningRoegtaethedOmdrejningstal : null) : null);
                    dataItem.VehicleEnvironmentRetrofitParticleFilter = (vehicleInfo != null ? (vehicleInfo.KoeretoejMiljoeOplysningStruktur != null ? vehicleInfo.KoeretoejMiljoeOplysningStruktur.KoeretoejMiljoeOplysningEftermonteretPartikelfilter.ToString() : null) : null);
                    dataItem.VehicleMotorCylinderCount = (vehicleInfo != null ? (vehicleInfo.KoeretoejMotorStruktur != null ? vehicleInfo.KoeretoejMotorStruktur.KoeretoejMotorCylinderAntal : null) : null);
                    dataItem.VehicleMotorEngineCapacity = (vehicleInfo != null ? (vehicleInfo.KoeretoejMotorStruktur != null ? vehicleInfo.KoeretoejMotorStruktur.KoeretoejMotorSlagVolumen.ToString() : null) : null);
                    dataItem.VehicleMotorBiggestEffect = (vehicleInfo != null ? (vehicleInfo.KoeretoejMotorStruktur != null ? vehicleInfo.KoeretoejMotorStruktur.KoeretoejMotorStoersteEffekt.ToString() : null) : null);
                    dataItem.VehicleMotorKmPerLiter = (vehicleInfo != null ? (vehicleInfo.KoeretoejMotorStruktur != null ? vehicleInfo.KoeretoejMotorStruktur.KoeretoejMotorKmPerLiter.ToString() : null) : null);
                    dataItem.VehicleMotorKmPerLiterPreCalc = (vehicleInfo != null ? (vehicleInfo.KoeretoejMotorStruktur != null ? vehicleInfo.KoeretoejMotorStruktur.KoeretoejMotorKMPerLiterPreCalc.ToString() : null) : null);
                    dataItem.VehicleMotorPlugInHybrid = (vehicleInfo != null ? (vehicleInfo.KoeretoejMotorStruktur != null ? vehicleInfo.KoeretoejMotorStruktur.KoeretoejMotorPlugInHybrid.ToString() : null) : null);
                    dataItem.VehicleMotorMileage = (vehicleInfo != null ? (vehicleInfo.KoeretoejMotorStruktur != null ? vehicleInfo.KoeretoejMotorStruktur.KoeretoejMotorKilometerstand : null) : null);
                    dataItem.VehicleMotorMileageDocumentation = (vehicleInfo != null ? (vehicleInfo.KoeretoejMotorStruktur != null ? vehicleInfo.KoeretoejMotorStruktur.KoeretoejMotorKilometerstandDokumentation.ToString() : null) : null);
                    dataItem.VehicleMotorMarking = (vehicleInfo != null ? (vehicleInfo.KoeretoejMotorStruktur != null ? vehicleInfo.KoeretoejMotorStruktur.KoeretoejMotorMaerkning : null) : null);
                    dataItem.VehicleMotorIdleSoundLevel = (vehicleInfo != null ? (vehicleInfo.KoeretoejMotorStruktur != null ? vehicleInfo.KoeretoejMotorStruktur.KoeretoejMotorStandStoej.ToString() : null) : null);
                    dataItem.VehicleMotorRunningSoundLevel = (vehicleInfo != null ? (vehicleInfo.KoeretoejMotorStruktur != null ? vehicleInfo.KoeretoejMotorStruktur.KoeretoejMotorKoerselStoej.ToString() : null) : null);
                    dataItem.VehicleMotorIdleSoundLevelSpeed = (vehicleInfo != null ? (vehicleInfo.KoeretoejMotorStruktur != null ? vehicleInfo.KoeretoejMotorStruktur.KoeretoejMotorStandStoejOmdrejningstal : null) : null);
                    dataItem.VehicleMotorInnovativeTech = (vehicleInfo != null ? (vehicleInfo.KoeretoejMotorStruktur != null ? vehicleInfo.KoeretoejMotorStruktur.KoeretoejMotorInnovativTeknik.ToString() : null) : null);
                    dataItem.VehicleMotorInnovativeTechCount = (vehicleInfo != null ? (vehicleInfo.KoeretoejMotorStruktur != null ? vehicleInfo.KoeretoejMotorStruktur.KoeretoejMotorInnovativTeknikAntal.ToString() : null) : null);
                    dataItem.VehicleMotorElectricalConsumption = (vehicleInfo != null ? (vehicleInfo.KoeretoejMotorStruktur != null ? vehicleInfo.KoeretoejMotorStruktur.KoeretoejMotorElektriskForbrug.ToString() : null) : null);
                    dataItem.VehicleMotorFuelMode = (vehicleInfo != null ? (vehicleInfo.KoeretoejMotorStruktur != null ? vehicleInfo.KoeretoejMotorStruktur.KoeretoejMotorFuelmode.ToString() : null) : null);
                    dataItem.VehicleMotorGasConsumption = (vehicleInfo != null ? (vehicleInfo.KoeretoejMotorStruktur != null ? vehicleInfo.KoeretoejMotorStruktur.KoeretoejMotorGasforbrug.ToString() : null) : null);
                    dataItem.VehicleMotorElectricalRange = (vehicleInfo != null ? (vehicleInfo.KoeretoejMotorStruktur != null ? vehicleInfo.KoeretoejMotorStruktur.KoeretoejMotorElektriskRaekkevidde.ToString() : null) : null);
                    dataItem.VehicleMotorBatteryCapacity = (vehicleInfo != null ? (vehicleInfo.KoeretoejMotorStruktur != null ? vehicleInfo.KoeretoejMotorStruktur.KoeretoejMotorBatterikapacitet.ToString() : null) : null);
                    dataItem.VehicleMotorDrivingForceNumber = (vehicleInfo != null ? (vehicleInfo.KoeretoejMotorStruktur != null ? (vehicleInfo.KoeretoejMotorStruktur.DrivkraftTypeStruktur != null ? vehicleInfo.KoeretoejMotorStruktur.DrivkraftTypeStruktur.DrivkraftTypeNummer : null) : null) : null);
                    dataItem.VehicleMotorDrivingForceName = (vehicleInfo != null ? (vehicleInfo.KoeretoejMotorStruktur != null ? (vehicleInfo.KoeretoejMotorStruktur.DrivkraftTypeStruktur != null ? vehicleInfo.KoeretoejMotorStruktur.DrivkraftTypeStruktur.DrivkraftTypeNavn : null) : null) : null);
                    dataItem.VehicleInspectionResultType = (statistic.SynResultatStruktur != null ? statistic.SynResultatStruktur.SynResultatSynsType.ToString() : null);
                    dataItem.VehicleInspectionResultDate = (statistic.SynResultatStruktur != null ? (statistic.SynResultatStruktur.SynResultatSynsDato != null ? statistic.SynResultatStruktur.SynResultatSynsDato : dfTime) : dfTime);
                    dataItem.VehicleInspectionResult = (statistic.SynResultatStruktur != null ? statistic.SynResultatStruktur.SynResultatSynsResultat.ToString() : null);
                    dataItem.VehicleInspectionResultStatus = (statistic.SynResultatStruktur != null ? statistic.SynResultatStruktur.SynResultatSynStatus.ToString() : null);
                    dataItem.VehicleInspectionResultStatusDate = (statistic.SynResultatStruktur != null ? (statistic.SynResultatStruktur.SynResultatSynStatusDato != null ? statistic.SynResultatStruktur.SynResultatSynStatusDato : dfTime) : dfTime);
                    dataItem.VehicleInspectionResultMileage = (statistic.SynResultatStruktur != null ? statistic.SynResultatStruktur.KoeretoejMotorKilometerstand : null);
                    dataItem.VehicleInspectionResultReinspectionMeetingDate = (statistic.SynResultatStruktur != null ? (statistic.SynResultatStruktur.SynResultatOmsynMoedeDato != null ? statistic.SynResultatStruktur.SynResultatOmsynMoedeDato : dfTime) : dfTime);
                    dataItem.VehicleRegistrationStatus = (statistic.KoeretoejRegistreringStatus.ToString());
                    dataItem.VehicleRegistrationDate = (statistic.KoeretoejRegistreringStatusDato != null ? statistic.KoeretoejRegistreringStatusDato : dfTime);

                    mrItems.Add(dataItem);

                    Console.WriteLine("[" + currentProccessedItem + "] Added " + statistic.RegistreringNummerNummer);

                    if (currentProccessedItem % SavingRowsInternal == 0)
                    {
                        bool updatedDb = false;
                        Console.WriteLine("Saving " + SavingRowsInternal + " records");

                        do
                        {
                            try
                            {
                                collection.InsertMany(mrItems);
                                updatedDb = true;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message + ", " + e.StackTrace);
                                Console.WriteLine("Error happened when trying to save records - Retrying 2 seconds");
                                Thread.Sleep(2000);
                            }
                        } while (updatedDb == false);

                        mrItems = new List<MotorregisterDataItem>();
                    }

                    currentProccessedItem++;
                }
                while (reader.ReadToNextSibling("ns:Statistik"));

                collection.InsertMany(mrItems);

                mrItems = new List<MotorregisterDataItem>();

                Console.WriteLine("Generation is done!");

                reader.Close();
            });
            thr.IsBackground = true;
            thr.Priority = ThreadPriority.Highest;
            thr.Start();
        }

        public void LoadSettings()
        {
            Console.Write("Enter Input File Name: ");
            InputFileName = Console.ReadLine();

            Console.Write("Enter amount of how many rows before save: ");
            SavingRowsInternal = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("- Press ENTER to Start Generator -");
            Console.ReadLine();
        }
    }
}
