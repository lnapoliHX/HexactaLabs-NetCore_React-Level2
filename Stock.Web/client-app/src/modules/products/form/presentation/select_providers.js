import React, {Component} from "react";
import api from "../../../../common/api";
import { Field } from "redux-form";
import SelectField from "../../../../components/inputs/SelectField";

class SelectProviders extends Component {
    _isMounted = false;

    state = {
        options: []
    }

    componentDidMount(){
        this._isMounted = true;
        api.get('/provider')
        .then(response => {
            if (this._isMounted) {
                this.setState({
                    options: this.state.options.concat([{ value: "", label: 'Seleccione Proveedor'}])
                });
                let a = response.data.map((each, index)=> {
                    return {
                    value : each.id,
                    label: each.name
                    }
                });
                this.setState({options:this.state.options.concat(a)});
            }
        })
        .catch(() => {
            return null;
        });
    }

    componentWillUnmount() {
        this._isMounted = false;
      }

    render() {    
    return (
        <Field label="Proveedor" options={this.state.options} name="providerId" component={SelectField} type ="select"></Field>
    );
  }
}

export default SelectProviders;