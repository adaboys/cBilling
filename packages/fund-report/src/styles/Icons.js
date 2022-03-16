import React from 'react';
import { renderToString } from 'react-dom/server';
import get from 'lodash/get';
let cache = {};
// eslint-disable-next-line
export const iconToMap = ({iconElement, color='#3f51b5', viewBox = '0 0 24 24', width = 32, height = width}) => {
  let fixIcon = typeof iconElement === 'function' ? React.createElement(iconElement) : iconElement;
  let key = get(fixIcon, 'props.cache') ? fixIcon.props.cache + color + viewBox + width + height : null;
  if (key && cache[key]) return cache[key];

  let data = renderToString(
    React.cloneElement(fixIcon, { xmlns: 'http://www.w3.org/2000/svg', width, height, viewBox }),
  );
  let style = `<style> svg {fill: ${color};} </style>`;
  // eslint-disable-next-line
  data = 'data:image/svg+xml;utf-8,' + data.replace(/<svg(.*?)>/g, math => {
      return math + style;
    });
  data = encodeURI(data);
  if (key) {
    cache[key] = data;
  }
  return data;
};

export {
  ManageAccountsIcon as ManageAccountsIcon,
  SwitchAccountIcon as SwitchAccountIcon,
  Dashboard as DashboardIcon,
  Map as DmaIcon,
  AccountBox as AppUserIcon,
  Group as PermissionIcon,
  SettingsOverscan as DesignSetupIcon,
  Polymer as ParentMenuMatIcon,
  InsertChart as ManageMatIcon,
  ViewList as StatisticMatInStkIcon,
  ViewList as StatisticMatByDmaIcon,
  ViewList as StatisticMatByLifeSpanIcon,
  Eject as ExportStockIcon,
  Print as PrintIcon,
  Stop as StopIcon,
  PlayArrow as ResumeIcon,
  Add as AddMatTypeIcon,
  ViewList as MatDetailTypeIcon,
  Memory as OtherIcon,
  Transform as StatisticButtonIcon,
  InsertChart as ParentMenuStatisticIcon,
  Settings as ConfigurationIcon,
  Web as MonitoringIcon,
  InsertChart as EmploymentIcon,
  InsertChart as TaskboardIcon,
  Error as RestoreMatIcon,
  CompareArrows as ReturnMatForStockIcon,
  FormatColorReset as creportLossIcon,
  Opacity as QuantityIcon,
  LocationCity as FactoryIcon,
  NetworkCheck as PressureIcon,
  BubbleChart as FlowRateIcon,
  Timeline as StatusIcon,
  LowPriority as creportFlowPressureIcon,
  AccessTime as LogTimeIcon,
  BubbleChart as MaterialOnMapIcon,
  Map as GeoIcon,
  LocationCity as GeoChildIcon,
  InsertChartOutlined as StatisticIcon,
  DeviceHub as ClientMeterIcon,
  Filter8 as ClientMeterNumberIcon,
  PersonAdd as ClientRegisterIcon,
  HowToReg as ClientContractIcon,
  Create as WriteMeterNumberIcon,
  LooksOne as ListClientMeterIcon,
  Build as ClientRequestIcon,
  ChromeReaderMode as CtmTemplateIcon,
  People as ClientIcon,
  Money as FormulaIcon,
  Build as ParentMenuSettingIcon,
  Build as ChangeClientMeterIcon,
  Add as SetupClientMeterIcon,
  Create as EditClientMeterIcon,
  Lockcon,
  Map as ParentMenuMapIcon,
  DeviceHub as ClientDistributionMapIcon,
  Lock as InvoiceLockIcon,
  History as InvoiceHistory,
  FileCopy as InvoiceExport,
  PeopleRounded as CustomerIcon,
  DeviceHub as ClientWritePayMapIcon,
  Home as CtmCompanyIcon,
  RecentActors as ReportQuantityClientIcon,
  ArtTrack as ParentMenuReportIcon,
  LibraryBooks as ReportDebtClientIcon,
  Subtitles as ReportRevenueLossClientIcon,
  Web as ReportIncomeIcon,
  PlaylistPlay as ReportRevenueLosscreportIcon,
  AttachMoney as RevenueIcon,
  CloudUpload as ImportIcon,
  InsertDriveFile as TemplateIcon,
  CloudDownload as ExportIcon,
  Business as BusinessIcon,
  Payment as InvoicePaymentIcon,
  DoneAll as SelectedsIcons,
  MonetizationOn as ClientDebtIcon,
  Done as CompleteIcon,
  Opacity as WidgetcreportQuantityIcon,
  Grain as WidgetcreportLossIcon,
  Email as TicketSupportIcon,
  Call as CommunicationIcon,
  Message as AnnouncementIcon,
  Business as ParentMenuEnterpriseIcon,
  Message as AgentIcon,
  Payment as PartnerIcon,
  DoneAll as QuotacreportIcon,
  Group as InstallationTeamIcon,
  Functions as RootMeterIcon,
  TableChart as ReportClientMeterIcon,
  LockOpen as ActiveButtonIcon,
  Warning as WarningIcon,
  Usb as InterfaceStandardIcon,
  Opacity as MeasureMethodIcon,
  SettingsOverscan as ParentMenuDesignIcon,
  SpellCheck as ParentMenuStandardIcon,
  DoneOutline as creportStandardIcon,
  FormatListNumbered as creportParameterIcon,
  FlipToBack as creportSourceGroupIcon,
  WavesRounded as creportSourceIcon,
  BorderColor as SensorIcon,
  Devices as DataLoggerIcon,
  AddAlert as AlertThresholdIcon,
  NotificationImportant as AlertcreportSourceIcon,
  DoneAll as NormalcreportSourceIcon,
  Repeat as ReportFlowIcon,
  PlaylistAddCheck as ReportQualityIcon,
  BarChart as ReportVolumeIcon,
  Opacity as ReportSummarizedQualityIcon,
  ViewHeadline as SourceFlowRateIcon,
  FormatListBulleted as ReportMaterialIcon,
  ChromeReaderMode as SourceTemplateIcon,
  Category as GisDesignIcon,
  Timeline as LineChartIcon,
  BarChart as BarChartIcon,
  DragIndicator as DetailIcon,
  BubbleChart as BubbleChartIcon,
  PowerInput as DesignPipeIcon,
  RoundedCorner as MapPipeIcon,
  Search as MonitorIcon,
} from '@material-ui/icons';

export { default as FilterIcon } from './svgs/FilterIcon';
export { default as FlowLoggerIcon } from './svgs/FlowLoggerIcon';
export { default as MeterIcon } from './svgs/MeterIcon';
export { default as NodeIcon } from './svgs/NodeIcon';
export { default as PipeIcon } from './svgs/PipeIcon';
export { default as PressureReducingIcon } from './svgs/PressureReducingIcon';
export { default as PumpIcon } from './svgs/PumpIcon';
export { default as QualityLoggerIcon } from './svgs/QualityLoggerIcon';
export { default as TankIcon } from './svgs/TankIcon';
export { default as ValveIcon } from './svgs/ValveIcon';
export { default as AddTaskboardIcon } from './svgs/AddTaskboardIcon';
export default {};
