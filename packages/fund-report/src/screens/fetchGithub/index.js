import { LineChartIcon } from '../../styles/Icons';
import Main from '../../resources/changepassword/main'
export default {
  name: 'ChangePassword',
  label: 'generic.pages.fetchgithub',
  icon: LineChartIcon,
  url: 'LineChartIcon',
  screens: {
    main: Main
  },
  resources: ['changepasswords'],
  active: true,
  access: {
    read: [],
    write: [],
  },
};
