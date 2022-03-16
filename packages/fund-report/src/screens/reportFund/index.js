import { RevenueIcon } from '../../styles/Icons';
import ListreportFund from '../../resources/reportFund/listreportFund';
import CreatereportFund from '../../resources/reportFund/createreportFund';
import EditreportFund from '../../resources/reportFund/editreportFund';
import ShowreportFund from '../../resources/reportFund/showreportFund';

export default {
  name: 'reportFund',
  label: 'generic.pages.reportFund',
  icon: RevenueIcon,
  url: 'fundhistory',
  screens: {
    list: ListreportFund,
    create: CreatereportFund,
    edit: EditreportFund,
    show: ShowreportFund,
  },
  resources: ['reportfunds'],
  active: true,
  access: {
    read: [],
    write: [],
  },
};
