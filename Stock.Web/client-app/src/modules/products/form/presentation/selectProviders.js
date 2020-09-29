import React, {Component} from "react";
import api from "../../../../common/api";
import { Field } from "redux-form";
import SelectField from "../../../../components/inputs/SelectField";

class SelectProviders extends Component {

    constructor(props) {
        super(props);
        this.state = {
          options: []
        }
      }

    componentDidMount(){
    
        api.get('/provider')
        .then(response => {
                this.setState({
                    options: this.state.options.concat([{ value: "", label: 'Seleccione Proveedor'}])
                });
                let proveedores = response.data.map(each=> {
                    return {
                        value : each.id,
                        label: each.name
                    }
                });
                this.setState({options:this.state.options.concat(proveedores)});
            
        })
    }

    render() {    
    return (
        <Field label="Proveedores" options={this.state.options} name="providerId" component={SelectField} type ="select"></Field>
    );
  }
}

export default SelectProviders; 