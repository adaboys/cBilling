import { RevenueIcon } from '../../styles/Icons';
import ReportKpi from './ReportKpi';

export default {
  name: 'reportcBilling',
  label: 'generic.pages.reportKpi',
  icon: RevenueIcon,
  url: 'reportcBilling',
  screens: {
    main: ReportKpi
  },
  resources: ['reportcBillings'],
  active: true,
  access: {
    read: [],
    write: [],
  },
};
