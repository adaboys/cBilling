import { RevenueIcon } from '../../styles/Icons';
import ListreportcBilling from '../../resources/reportcBilling/listreportcBilling';
import CreatereportcBilling from '../../resources/reportcBilling/createreportcBilling';
import EditreportcBilling from '../../resources/reportcBilling/editreportcBilling';
import ShowreportcBilling from '../../resources/reportcBilling/showreportcBilling';

export default {
  name: 'reportcBilling',
  label: 'generic.pages.reportcBilling',
  icon: RevenueIcon,
  url: 'cBillinghistory',
  screens: {
    list: ListreportcBilling,
    create: CreatereportcBilling,
    edit: EditreportcBilling,
    show: ShowreportcBilling,
  },
  resources: ['reportcBillings'],
  active: true,
  access: {
    read: [],
    write: [],
  },
};
