import { RevenueIcon } from '../../styles/Icons';
import ReportKpi from './ReportKpi';

export default {
  name: 'reportFund',
  label: 'generic.pages.reportKpi',
  icon: RevenueIcon,
  url: 'reportFund',
  screens: {
    main: ReportKpi
  },
  resources: ['reportfunds'],
  active: true,
  access: {
    read: [],
    write: [],
  },
};
