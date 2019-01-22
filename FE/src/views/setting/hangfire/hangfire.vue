<template>
    <Button @click="create" icon="android-add" type="primary" size="large">查看详情</Button>
</template>
<script lang="ts">
    import { Component, Vue,Inject, Prop,Watch } from 'vue-property-decorator';
    import Util from '@/lib/util';
    import AbpBase from '@/lib/abpbase';
    import url from '@/lib/url';

    export default class Hangfire extends AbpBase {
        create(){
            window.open(url+"hangfire","_blank");
        }
    }
</script>
